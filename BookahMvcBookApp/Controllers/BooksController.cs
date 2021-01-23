using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookahMvcBookApp.Models;

namespace BookahMvcBookApp.Controllers
{
    public class BooksController : Controller
    {
        dbBookahMvcBookAppEntities db = new dbBookahMvcBookAppEntities();

        // GET: Books
        public ActionResult Index()
        {
            return View(db.tblBooks.ToList());
        }

        public ActionResult Create()
        {
            List<tblCategory> list = db.tblCategories.ToList();
            ViewBag.catList = new SelectList(list, "CatId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create( tblBook b, HttpPostedFileBase Picture)
        {
            List<tblCategory> list = db.tblCategories.ToList();
            ViewBag.CatList = new SelectList(list, "CatId", "Name");

            if (ModelState.IsValid)
            {

                tblBook book = new tblBook();
                book.Author = b.Author;
                book.Description = b.Description;
                book.Edition = b.Edition;
                book.ISBN = b.ISBN;
                book.Publisher = b.Publisher;
                book.Picture = Picture.FileName.ToString();
                book.Title = b.Title;
                book.Unit = b.Unit;

                //Picture Upload
                var folder = Server.MapPath("~/Uploads/");
                Picture.SaveAs(Path.Combine(folder, Picture.FileName.ToString()));

                db.tblBooks.Add(book);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {
                TempData["msg"] = "Book Not Uploaded";

            }
            return View();
        }
    }
}