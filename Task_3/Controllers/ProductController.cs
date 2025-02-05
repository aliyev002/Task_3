using Microsoft.AspNetCore.Mvc;
using Task_3.Entities;
using Task_3.Services;

namespace Task_3.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                product.ImagePath = $"images/{fileName}";
            }

            await _productService.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllByKeyAsync();
            return View(products);
            
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = (await _productService.GetAllByKeyAsync()).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        

        [HttpPost]
        public async Task<IActionResult> Update(Product product, IFormFile ImageFile)
        {
            var existingProduct = await _productService.GetByIdAsync(product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Discount = product.Discount;
            existingProduct.Price = product.Price;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                existingProduct.ImagePath = $"images/{fileName}";
            }

            await _productService.UpdateAsync(existingProduct);
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Delete(int id)
        {
            var product = (await _productService.GetAllByKeyAsync()).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
