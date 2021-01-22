using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookahMvcBookApp.Models;

namespace BookahMvcBookApp.Controllers
{
    public class CategoryController : Controller
    {
        dbBookahMvcBookAppEntities db = new dbBookahMvcBookAppEntities();

        public ActionResult Index()
        {
            var query = db.tblCategories.ToList();
            return View(query);
        }
        // GET: Category
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblCategory c)
        {
            if(ModelState.IsValid)
            {
                tblCategory cat = new tblCategory();
                cat.Name = c.Name;
                db.tblCategories.Add(cat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "Category not inserted.";
            }
            return View();
        }
    }
}