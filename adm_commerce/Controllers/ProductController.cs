using adm_commerce.Data;
using adm_commerce.Entities;
using adm_commerce.Models;
using areas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace adm_commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly FileService _fileService;

        public ProductController(AppDbContext dbContext, FileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        public IActionResult Index()
        {
            var products = _dbContext.Products.
           Include(x => x.ProductImage)
           .Include(x => x.Category)
           .ToList();

            var model = new ProductIndexVM
            {
                Products = products
            };

            return View(model);
        }

        public IActionResult Add()
        {
            var model = new ProductAddVM();

            var productTypes = _dbContext.Categories.ToList();


            model.Category = productTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }


        [HttpPost]
        public IActionResult Add(ProductAddVM model)
        {
            if (ModelState.IsValid)
            {
                var productTypes = _dbContext.Categories.ToList();
                model.Category = productTypes.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

                return View(model);
            };

            var imageName = _fileService.UploadFile(model.Photo);

            var product = new Product
            {
                Name = model.Name,
                Price = (decimal)model.Price,
                CategoryId = model.ProductCategoryId,
                ProductImage = new ProductImage
                {
                    ImageName = imageName
                }
            };

            _dbContext.Products.Add(product);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        public IActionResult Delete(int id)
        {
            var product = _dbContext.Products.Include(x => x.ProductImage).FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            if (product.ProductImage != null)
            {
                _fileService.DeleteFile(product.ProductImage.ImageName);
            }

            _dbContext.Products.Remove(product);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            var product = _dbContext.Products
                .Include(x => x.ProductImage)
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            var productTypes = _dbContext.Categories.ToList();

            var model = new ProductUpdateVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageName = product.ProductImage?.ImageName,
                Category = productTypes.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                ProductCategoryId = product.Category.Id
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateVM model)
        {
            if (ModelState.IsValid) return View(model);

            var product = _dbContext.Products.Include(x => x.ProductImage).FirstOrDefault(x => x.Id == model.Id);
            if (product is null) return NotFound();

            if (model.Photo != null)
            {
                if (product.ProductImage != null)
                {
                    _fileService.DeleteFile(product.ProductImage.ImageName);
                }

                product.ProductImage.ImageName = _fileService.UploadFile(model.Photo);
            }

            product.Name = model.Name;
            product.Price = (decimal)model.Price;
            product.CategoryId = model.ProductCategoryId;

            _dbContext.Products.Update(product);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



    }



}
