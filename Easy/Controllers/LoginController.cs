using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Models;


namespace Easy.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(Easy.Models.User userModel)
        {
            using (AppEntities db = new AppEntities())
            {
                var userDetails = db.User.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                   
                    userModel.LoginErrorMessage = "Nom d'utilisateur ou mot de passe incorrecte ";
                    return View("index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserName;

                    return RedirectToAction("Index", "Home");
                }
            }

        }
        public ActionResult Logout()
        {
            int User1 = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }
    }
}