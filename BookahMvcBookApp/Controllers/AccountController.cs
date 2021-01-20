using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BookahMvcBookApp.Models;

namespace BookahMvcBookApp.Controllers
{
    public class AccountController : Controller
    {
        dbBookahMvcBookAppEntities db = new dbBookahMvcBookAppEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tblUser t)
        {
            var query = db.tblUsers.SingleOrDefault(m => m.Email == t.Email && m.Password == t.Password);
            if (query != null)
            {
                if (query.RoleType == 1)
                {
                    Session["uid"] = query.UserId;
                    FormsAuthentication.SetAuthCookie(query.Email, false);
                    Session["User"] = query.Email;
                    return RedirectToAction("Index", "Home");
                }
                else if (query.RoleType == 2)
                {
                    Session["uid"] = query.UserId;
                    FormsAuthentication.SetAuthCookie(query.Email, false);
                    Session["User"] = query.Email;
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                TempData["msg"] = "Invalid Username or Password";
            }

            return View();
        }

    }
}