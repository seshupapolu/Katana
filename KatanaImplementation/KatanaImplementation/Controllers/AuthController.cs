using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KatanaImplementation.Models;
using System.Security.Claims;

namespace KatanaImplementation.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {

            return View(new LoginModel());
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (model.Username == "seshu" && model.Password == "password")
            {
                var identity = new ClaimsIdentity("ApplicationCookie");
                identity.AddClaims(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,model.Username),
                    new Claim(ClaimTypes.Name,model.Username),

                });

                HttpContext.GetOwinContext().Authentication.SignIn(identity);
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/Auth/Login");
        }
    }
}