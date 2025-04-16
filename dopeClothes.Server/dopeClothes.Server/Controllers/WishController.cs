using dopeClothes.Server.IRepositories;
using dopeClothes.Server.Models;
using dopeClothes.Server.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dopeClothes.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WishController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public WishController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("InsertProduct/{productId:int}")]
        [HttpPost]
        public IActionResult Create(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var whishList = _unitOfWork.WishLists.Get(x => x.UserId == userId);
                if (whishList == null)
                {
                    whishList = new WhishList()
                    {
                        UserId = userId,

                    };
                    _unitOfWork.WishLists.Add(whishList);
                    _unitOfWork.Save();
                }
                var product = _unitOfWork.Products.Get(x => x.Id == productId);
                if (product == null)
                {
                    return BadRequest(new { message = "Product not found" });
                }
                var wishListItem = _unitOfWork.WishListItems.Get(x => x.ProductId == productId && x.WishListId == whishList.Id);
                if (wishListItem != null) return BadRequest(new { message = "Product already in!" });
                else
                {
                    WishListItem itemToDb = new WishListItem()
                    {
                        ProductId = productId,
                        WishListId = whishList.Id,
                    };
                    _unitOfWork.WishListItems.Add(itemToDb);
                    _unitOfWork.Save();
                }
                return Ok(new { message = "Product successfully added to wishlist" });
            }
            else
            {
                return BadRequest(new { message = "Not signed in!" });
            }

        }
        [HttpGet("GetMyWishList")]
        public IActionResult GetMyWishList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var whishList = _unitOfWork.WishLists.Get(x => x.UserId == userId);
                if (whishList != null)
                {
                    var listOfItems = _unitOfWork.WishListItems.GetAll(x => x.WishListId == whishList.Id, includeProperties: "Product");
                    return Ok(new { listOfItems = listOfItems });
                }
                else
                {
                    return BadRequest(new { message = "Nothing in there" });
                }
            }
            else
            {
                return BadRequest(new { message = "Not signed in!" });
            }
        }
        [HttpPost("DeleteProductFromWishList/{productiD:int}")]

        public IActionResult DeleteFromWishList(int productiD)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var whishList = _unitOfWork.WishLists.Get(x => x.UserId == userId);
                if (whishList != null)
                {
                    var wishListItem = _unitOfWork.WishListItems.Get(x => x.ProductId == productiD && x.WishListId == whishList.Id);
                    if (wishListItem != null)
                    {
                        _unitOfWork.WishListItems.Delete(wishListItem);
                        _unitOfWork.Save();
                        return Ok(new { message = "Product successfully removed from wishlist" });
                    }
                    else
                    {
                        return BadRequest(new { message = "Product not found in wishlist" });
                    }
                }
                else
                {
                    return BadRequest(new { message = "Nothing in there" });
                }
            }
            else
            {
                return BadRequest(new { message = "Not signed in!" });
            }

        }
       
    }
}

