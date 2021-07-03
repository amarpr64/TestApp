using DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        DatabaseContext _db;
        public ProductController(DatabaseContext db)
        {
            //_db = new DatabaseContext();
            _db = db;
        }
        public IActionResult Index()
        {
            //var data = _db.Products.ToList();
            //OR
            //var data = _db.Products.Select(p => p).ToList();
            //OR
            //var data = (from prd in _db.Products
            //            select prd).ToList();

            var data = (from prd in _db.Products
                        join cat in _db.Categories
                        on prd.CategoryId equals cat.CategoryId
                        select new ProductModel
                        {
                            ProductId = prd.ProductId,
                            Name = prd.Name,
                            UnitPrice=prd.UnitPrice,
                            Description = prd.Description,
                            Category = cat.Name
                        }).ToList();

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = _db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            ModelState.Remove("ProductId");
            if (ModelState.IsValid)
            {
                _db.Products.Add(model);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _db.Categories.ToList();
            return View();
        }

        public IActionResult Edit(int id)
        {
            //Product model = _db.Products.Find(id);
            //Product model = _db.usp_getproduct(id);
            Product model = _db.udf_getproduct(id);

            ViewBag.CategoryList = _db.Categories.ToList();
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(model);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _db.Categories.ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int id)
        {
            Product model = _db.Products.Find(id);
            if (model != null)
            {
                _db.Products.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
