using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Models;

namespace Easy.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Register(int id=0)
        {
            User userModel = new User();
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User userModel)
        {
            using (AppEntities db = new AppEntities())
            {


                if (db.User.Any(x => x.UserName == userModel.UserName))
                {
                    ViewBag.DuplicateMessage = "Utilisateurs existant";
                    return View("Register", userModel);
                }
                db.User.Add(userModel);

                db.SaveChanges();

            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Inscription réussi avec succès";

            return View("Register", new User());


        }
    }
}