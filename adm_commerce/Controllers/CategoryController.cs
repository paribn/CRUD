using adm_commerce.Data;
using adm_commerce.Entities;
using adm_commerce.Models;
using areas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace adm_commerce.Controllers
{
    public class CategoryController : Controller
    {

        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var categories = _dbContext.Categories
           .ToList();

            var model = new CategoryIndexVM
            {
                 Categories= categories
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
              Name=model.Name
             
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
