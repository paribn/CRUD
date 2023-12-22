using adm_commerce.Areas.Admin.Data;
using adm_commerce.Areas.Admin.Models.CustomerVM;
using adm_commerce.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace adm_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class CustomerController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CustomerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            int page = 1;
            int defaultTake = 3;

            decimal productCount = _dbContext.Products.Count();

            int pageCount = (int)Math.Ceiling(productCount / defaultTake);

            var product = _dbContext.Customers.OrderByDescending(x => x.Id)
                .Skip((page - 1) * defaultTake)
                .Take(defaultTake)
                .ToList();

            var customers = _dbContext.Customers
           .ToList();

            var model = new CustomerIndexVM
            {
                Customers = customers,
                PageCount = pageCount
            };

            return View(model);
        }

        public IActionResult Add()
        {
            var model = new CustomerAddVM();

            var customers = _dbContext.Customers.ToList();

            return View(model);
        }


        [HttpPost]
        public IActionResult Add(CustomerAddVM model)
        {
            if (!ModelState.IsValid)
            {
                var customers = _dbContext.Customers.ToList();

                return View(model);
            };


            var customer = new Customer
            {
                FirstName = model.FistName,
                LastName = model.LastName,
                Email = model.Email

            };

            _dbContext.Customers.Add(customer);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(x => x.Id == id);

            if (customer is null) return NotFound();

            _dbContext.Customers.Remove(customer);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var customer = _dbContext.Customers
                .FirstOrDefault(x => x.Id == id);

            if (customer is null) return NotFound();


            var model = new CustomerUpdateVM
            {
                Id = customer.Id,
                FistName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CustomerUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var customer = _dbContext.Customers.FirstOrDefault(x => x.Id == model.Id);
            if (customer is null) return NotFound();


            customer.FirstName = model.FistName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;


            _dbContext.Customers.Update(customer);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
