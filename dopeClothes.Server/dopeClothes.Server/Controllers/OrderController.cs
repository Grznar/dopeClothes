using dopeClothes.Server.IRepositories;
using dopeClothes.Server.Models;
using dopeClothes.Server.Models.VMs;
using dopeClothes.Server.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dopeClothes.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

     
        public class StatusUpdateDto
        {
            public string Status { get; set; }
        }
        [HttpGet("GetMyOrders")]
        [Authorize]   // přihlášený uživatel
        public IActionResult GetMyOrders()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return BadRequest(new { message = "User not found" });

               
                var myOrders = _unitOfWork.Orders
                    .GetAll(includeProperties: "ShoppingCart.ShoppingCartItems.Product")
                    .Where(o => o.UserId == userId)
                    .Select(o => new MyOrderVM
                    {
                        OrderId = o.Id,
                        Date = DateTime.UtcNow,  
                        Status = o.OrderStatus ?? "Pending",
                        Total = o.ShoppingCart
                                      .ShoppingCartItems
                                      .Sum(i =>
                                        (i.Product.SalePrice > 0
                                           ? i.Product.SalePrice
                                           : i.Product.Price)
                                        * i.Count)
                    })
                    .ToList();

                if (!myOrders.Any())
                    return NotFound(new { message = "You have no orders." });

                return Ok(myOrders);
            }
            catch (Exception ex)
            {
                // log ex…
                return StatusCode(500, new { message = "Server error", detail = ex.Message });
            }
        }

        [HttpGet("GetAllOrders")]
        [Authorize(Roles = SD.ROLE_ADMIN)]
        public IActionResult GetAllOrders()
        {
            try
            {
                var dtos = _unitOfWork.Orders
                    .GetAll(includeProperties: "ShoppingCart.ApplicationUser,ShoppingCart.ShoppingCartItems.Product")
                    .Select(o => new AdminOrderVM
                    {
                        OrderId = o.Id,
                        UserEmail = o.ShoppingCart.ApplicationUser.Email,
                        Date = o.ShoppingCart.Id == o.ShoppingCartId
                                     ? DateTime.UtcNow
                                     : DateTime.UtcNow,
                        Status = o.OrderStatus ?? "Pending",
                        Total = o.ShoppingCart.ShoppingCartItems
                                      .Sum(i => (i.Product.SalePrice > 0
                                                 ? i.Product.SalePrice
                                                 : i.Product.Price) * i.Count)
                    })
                    .ToList();

                if (!dtos.Any())
                    return NotFound(new { message = "No orders found." });

                return Ok(dtos);   
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error", detail = ex.Message });
            }
        }



        [HttpGet("GetOrder/{shoppingCartId:int}")]
        public IActionResult GetOrder(int shoppingCartId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return BadRequest(new { message = "User not found" });

                var order = _unitOfWork.Orders
                    .Get(o => o.ShoppingCartId == shoppingCartId && o.UserId == userId);
                if (order == null)
                    return NotFound(new { message = "Order not found" });

                return Ok(new { order });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { message = "Server error", detail = ex.Message });
            }
        }

        [HttpPost("PlaceOrder/{shoppingCartId:int}")]
        public IActionResult PlaceOrder(int shoppingCartId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return BadRequest(new { message = "User not found" });

                var existing = _unitOfWork.Orders
                    .Get(o => o.ShoppingCartId == shoppingCartId && o.UserId == userId);
                if (existing != null)
                    return BadRequest(new { message = "Order already placed" });

              
                var order = new Order
                {
                    UserId = userId,
                    ShoppingCartId = shoppingCartId,
                    OrderStatus = "Pending"
                };
                _unitOfWork.Orders.Add(order);
                _unitOfWork.Save();

                return Ok(new { message = "Order placed", orderId = order.Id });
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { message = "Server error", detail = ex.Message });
            }
        }

       
        [HttpPut("UpdateOrderStatus/{orderId:int}")]
        [Authorize(Roles = SD.ROLE_ADMIN)]
        public IActionResult UpdateOrderStatus(int orderId, [FromBody] StatusUpdateDto dto)
        {
            try
            {
                var order = _unitOfWork.Orders.Get(o => o.Id == orderId);
                if (order == null)
                    return NotFound(new { message = "Order not found" });

                order.OrderStatus = dto.Status;
                _unitOfWork.Orders.Update(order);
                _unitOfWork.Save();

                return Ok(new { message = "Status updated", order });
            }
            catch (Exception ex)
            {
          
                return StatusCode(500, new { message = "Server error", detail = ex.Message });
            }
        }
    }
}
