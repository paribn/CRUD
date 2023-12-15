﻿using adm_commerce.Data;
using adm_commerce.Entities;
using adm_commerce.Models;
using areas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace adm_commerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _dbContext;

        public OrderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var orders = _dbContext.Order.Include(x=> x.Product).Include(x=> x.Customer)
           .ToList();

            var model = new OrderIndexVM
            {
                Orders = orders  
            };

            return View(model);
        }

        public IActionResult Add()
        {
            var model = new OrderAddVM();

            var product = _dbContext.Products.ToList();
            var customer =_dbContext.Customers.ToList();

            model.Product = product.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Customer = customer.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.Id.ToString()
            }).ToList();


            return View(model);
        }

        [HttpPost]
        public IActionResult Add(OrderAddVM model)
        {
            if (ModelState.IsValid){

                var products = _dbContext.Products.ToList();
                var customers = _dbContext.Customers.ToList();

                model.Product = products.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

                model.Customer = customers.Select(x => new SelectListItem
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.Id.ToString()
                }).ToList();
                return View(model);
            }
          

            var order = new Order
            {
                ProductId=model.ProductId,
                CustomerId=model.CustomerId,
                OrderDate=(DateTime)model.CreationDate
        };

            _dbContext.Order.Add(order);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var order = _dbContext.Order.Include(x => x.Product).Include(x=> x.Customer).FirstOrDefault(x => x.Id == id);

            if (order is null) return NotFound();

            _dbContext.Order.Remove(order);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var product = _dbContext.Order
                .Include(x => x.Product)
                .Include(x => x.Customer)
                .FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            var productTypes = _dbContext.Products.ToList();
            var customers = _dbContext.Customers.ToList();


            var model = new OrderUpdateVM
            {
                Id = product.Id,
                ProductId = product.Product.Id,
                CustomerId = product.Customer.Id,
                Product = productTypes.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                Customer = customers.Select(x => new SelectListItem
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.Id.ToString()
                }).ToList(),

            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Update(OrderUpdateVM model)
        {
            if (ModelState.IsValid) return View(model);

            var order = _dbContext.Order.FirstOrDefault(x => x.Id == model.Id);
            if (order is null) return NotFound();

            order.ProductId = model.ProductId;
            order.CustomerId = model.CustomerId;

            _dbContext.Order.Update(order);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
