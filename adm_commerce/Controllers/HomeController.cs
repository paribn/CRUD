using adm_commerce.Areas.Admin.Data;
using adm_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace adm_commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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

            var products = _dbContext.Products
            .Include(x => x.ProductImage)
           .Include(x => x.Category)
           .ToList();

            var model = new HomeIndexVM
            {
                Products = products,
                PageCount = pageCount,
                CurrentPage = page,
            };

            return View(model);
        }

    }
}
