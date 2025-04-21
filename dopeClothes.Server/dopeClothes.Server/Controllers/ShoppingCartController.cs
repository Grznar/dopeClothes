using dopeClothes.Server.IRepositories;
using dopeClothes.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dopeClothes.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("InsertProductToShopCart/{productId:int}")]
        public IActionResult InsertProductToShopCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return BadRequest(new { message = "User not found" });
            var shoppingCart = _unitOfWork.ShoppingCarts.Get(x => x.UserId == userId);
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart()
                {
                    UserId = userId,
                };
                _unitOfWork.ShoppingCarts.Add(shoppingCart);
                _unitOfWork.Save();
            }
            var product = _unitOfWork.Products.Get(x => x.Id == productId);
            if (product != null)
            {
                var shoppingCartItem = _unitOfWork.ShoppingCartItems.Get(x => x.ProductId == productId && x.ShoppingCartId == shoppingCart.Id);
                if (shoppingCartItem != null)
                {
                    shoppingCartItem.Count++;
                    _unitOfWork.ShoppingCartItems.Update(shoppingCartItem);
                    _unitOfWork.Save();
                }
                else
                {
                    ShoppingCartItem itemToDb = new ShoppingCartItem()
                    {
                        ProductId = productId,
                        ShoppingCartId = shoppingCart.Id,
                        Count = 1
                    };
                    _unitOfWork.ShoppingCartItems.Add(itemToDb);
                    _unitOfWork.Save();
                }
                return Ok(new { message = "Product successfully added to shopping cart" });
            }
            else
            {
                return BadRequest(new { message = "Product not found" });
            }


        }
        [HttpGet("FetchShopCart")]
        public IActionResult FetchShopCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return BadRequest(new { message = "User not found" });
            var shoppingCart = _unitOfWork.ShoppingCarts.Get(x => x.UserId == userId);
            if (shoppingCart == null) return Ok(new { message = "Shop some items first!" });
            var items = _unitOfWork.ShoppingCartItems.GetAll(x => x.ShoppingCartId == shoppingCart.Id, includeProperties: "Product");
            if (items == null) return Ok(new { message = "No items in shopping cart" });
            return Ok(new { items = items });


        }
        [HttpPost("DecreaseProduct/{productId:int}")]
        public IActionResult DecreaseProduct(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return BadRequest(new { message = "User not found" });
            var shoppingCart = _unitOfWork.ShoppingCarts.Get(x => x.UserId == userId);
            if (shoppingCart == null) return BadRequest(new { message = "Shop some items first!" });
            var shoppingCartItem = _unitOfWork.ShoppingCartItems.Get(x => x.ProductId == productId && x.ShoppingCartId == shoppingCart.Id);
            if (shoppingCartItem == null) return BadRequest(new { message = "Product not found in shopping cart" });
            if (shoppingCartItem.Count > 1)
            {
                shoppingCartItem.Count--;
                _unitOfWork.ShoppingCartItems.Update(shoppingCartItem);
                _unitOfWork.Save();
                return Ok(new { message = "Product count decreased" });
            }
            else
            {
                _unitOfWork.ShoppingCartItems.Delete(shoppingCartItem);
                _unitOfWork.Save();
                return Ok(new { message = "Product removed from shopping cart" });
            }
        }
        [HttpPost("IncreaseProduct/{productId:int}")]
        public IActionResult IncreaseProduct(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Ok(new { message = "User not found" });
            var shoppingCart = _unitOfWork.ShoppingCarts.Get(x => x.UserId == userId);
            if (shoppingCart == null) return Ok(new { message = "Shop some items first!" });
            var shoppingCartItem = _unitOfWork.ShoppingCartItems.Get(x => x.ProductId == productId && x.ShoppingCartId == shoppingCart.Id);
            if (shoppingCartItem == null) return Ok(new { message = "Product not found in shopping cart" });
            if (shoppingCartItem.Count > 0)
            {
                shoppingCartItem.Count++;
                _unitOfWork.ShoppingCartItems.Update(shoppingCartItem);
                _unitOfWork.Save();
                return Ok(new { message = "Product count decreased" });
            }
            else
            {
                ShoppingCartItem shoppingCartItem1 = new ShoppingCartItem()
                {
                    ProductId = productId,
                    ShoppingCartId = shoppingCart.Id,
                    Count = 1
                };
                _unitOfWork.ShoppingCartItems.Add(shoppingCartItem1);
                _unitOfWork.Save();
                return Ok(new { message = "Product removed from shopping cart" });
            }
        }

    }
}
