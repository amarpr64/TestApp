using DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        DatabaseContext _db;
        public ProductController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var products = (from prd in _db.Products
            //                where prd.id>0  
            //                select prd).ToList();

            //var products = _db.Products.ToList();
            var products = _db.Products.Where(p => p.Id > 0).ToList();

            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                _db.Products.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _db.Categories.ToList();
            //Product model = _db.Products.Find(id);
            //Product model = _db.usp_getproduct(id);
            Product model = _db.fn_getproduct(id);

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
            ViewBag.Categories = _db.Categories.ToList();
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {
            _uow.ProductRepo.Add(model);
            _uow.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, model);
        }
        [HttpPut("{Id}")]
        public IActionResult Update(int Id, Product model)
        {
            if (Id != model.Id)
                return BadRequest();
            _uow.ProductRepo.Update(model);
            _uow.SaveChanges();
            return Ok();
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
