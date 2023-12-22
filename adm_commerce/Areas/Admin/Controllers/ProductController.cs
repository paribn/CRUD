using adm_commerce.Areas.Admin.Data;
using adm_commerce.Areas.Admin.Models;
using adm_commerce.Entities;
using areas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace adm_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
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
            int page = 1;
            int defaultTake = 3;

            decimal productCount = _dbContext.Products.Count();

            int pageCount = (int)Math.Ceiling(productCount / defaultTake);

            var product = _dbContext.Products.OrderByDescending(x => x.Id)
                .Skip((page - 1) * defaultTake)
                .Take(defaultTake)
                .ToList();

            var products = _dbContext.Products.
           Include(x => x.ProductImage)
           .Include(x => x.Category)
           .ToList();

            var model = new ProductIndexVM
            {
                Products = products,
                PageCount = pageCount,
                CurrentPage = page,
            };

            return View(model);
        }

        [Authorize(Roles = "Admin,OrdinaryUser")]
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
        [Authorize(Roles = "Admin,OrdinaryUser")]
        public IActionResult Add(ProductAddVM model)
        {
            if (!ModelState.IsValid)
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


        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
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
        [Authorize(Roles = "Admin,OrdinaryUser")]
        public IActionResult Update(ProductUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

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
