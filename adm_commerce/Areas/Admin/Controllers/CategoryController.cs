using adm_commerce.Areas.Admin.Data;
using adm_commerce.Areas.Admin.Models.CategoryVM;
using adm_commerce.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace adm_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class CategoryController : Controller
    {

        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            int page = 1;
            int defaultTake = 3;

            decimal productCount = _dbContext.Products.Count();

            int pageCount = (int)Math.Ceiling(productCount / defaultTake);

            var product = _dbContext.Categories.OrderByDescending(x => x.Id)
                .Skip((page - 1) * defaultTake)
                .Take(defaultTake)
                .ToList();

            var products = _dbContext.Products.
           Include(x => x.ProductImage)
           .Include(x => x.Category)
           .ToList();

            var categories = _dbContext.Categories
           .ToList();

            var model = new CategoryIndexVM
            {
                Categories = categories,
                PageCount = pageCount,
            };

            return View(model);
        }


        public IActionResult Add()
        {
            var model = new CategoryAddVM();



            return View(model);
        }


        [HttpPost]
        public IActionResult Add(CategoryAddVM model)
        {
            if (!ModelState.IsValid) return View(model);


            var category = new Category
            {
                Name = model.Name

            };

            _dbContext.Categories.Add(category);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Include(x => x.Products).FirstOrDefault(x => x.Id == id);

            if (category is null) return NotFound();

            _dbContext.Categories.Remove(category);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            var category = _dbContext.Categories
                .FirstOrDefault(x => x.Id == id);

            if (category is null) return NotFound();


            var model = new CategoryUpdateVM
            {
                Id = category.Id,
                Name = category.Name,

            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CategoryUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == model.Id);
            if (category is null) return NotFound();


            category.Name = model.Name;

            _dbContext.Categories.Update(category);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
