using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using BookahMvcBookApp.Models;

namespace BookahMvcBookApp.Controllers
{
    public class AccountController : Controller
    {
        dbBookahMvcBookAppEntities db = new dbBookahMvcBookAppEntities();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(tblUser t)
        {
            tblUser u = new tblUser();
            if(ModelState.IsValid)
            {
                u.Firstname = t.Firstname;
                u.Lastname = t.Lastname;
                u.Email = t.Email;
                u.IDNumber = t.IDNumber;
                u.PhoneNumber = t.PhoneNumber;
                u.Password = t.Password;
                u.RoleType = 2;
                db.tblUsers.Add(u);
                db.SaveChanges();

                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["msg"] = "You are not registered!!";
            }

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
            if(query != null)
            {             
                if(query.RoleType == 1)
                {
                    FormsAuthentication.SetAuthCookie(query.Email, false);
                    Session["User"] = query.Firstname;
                    return RedirectToAction("Index", "Home");
                }

                else if (query.RoleType == 2)
                {
                    FormsAuthentication.SetAuthCookie(query.Email, false);
                    Session["User"] = query.Firstname;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["msg"] = "Invalid Username or Password";
            }
            return View();
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
    }
}