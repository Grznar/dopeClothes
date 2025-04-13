using dopeClothes.Server.IRepositories;
using dopeClothes.Server.Models;
using dopeClothes.Server.Models.VMs;
using Microsoft.AspNetCore.Mvc;

namespace dopeClothes.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromForm] ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    SalePrice = productVM.SalePrice,

                };
                if (productVM.Image != null && productVM.Image.Length > 0)
                {
                    var wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(productVM.Image.FileName);
                    string path = Path.Combine(wwwRootPath, "images", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        productVM.Image.CopyTo(fileStream);
                    }
                    product.ImageUrl = "/images/" + fileName;
                }
                _unitOfWork.Products.Add(product);
                _unitOfWork.Save();
                return StatusCode(201);
            }
            else
            {
                var errors = ModelState.Values
       .SelectMany(v => v.Errors)
       .Select(e => e.ErrorMessage)
       .ToList();

                return BadRequest(errors);
            }
        }
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _unitOfWork.Products.GetAll();
            return Ok(products);
        }
        [HttpGet("Get/{id:int}")]
        public IActionResult Get(int id)
        {
            var product = _unitOfWork.Products.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost("Update")]
        public IActionResult Update([FromForm] ProductVM productVm)
        {
            if (ModelState.IsValid)
            {
                var product = _unitOfWork.Products.Get(u => u.Id == productVm.Id);
                if (product == null)
                {
                    return NotFound();
                }
                product.Name = productVm.Name;
                product.Description = productVm.Description;
                product.Price = productVm.Price;
                product.SalePrice = productVm.SalePrice;

           
                if (productVm.Image != null && productVm.Image.Length > 0)
                {
                    var wwwRootPath = _webHostEnvironment.WebRootPath;

                 
                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        var oldPath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(productVm.Image.FileName);
                    string path = Path.Combine(wwwRootPath, "images", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        productVm.Image.CopyTo(fileStream);
                    }
                    product.ImageUrl = "/images/" + fileName;
                }
                

                _unitOfWork.Products.Update(product);
                _unitOfWork.Save();
                return Ok(product);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
