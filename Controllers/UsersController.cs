using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevenMouths.Models;
using SevenMouths.Helpers;

namespace SevenMouths.Controllers
{
    
    public class UsersController : BaseController
    {
        [LoginFilter]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        { 
            string emailOrName = collection["emailOrName"];
            string password = collection["password"];

            User user = context.Users.FirstOrDefault(x => x.Name == emailOrName || x.Email == emailOrName);
            if (user != null)
            {
                if (user.Password == password)
                {
                    cookie["UserId", true, false] = user.UserId.ToString();
                    return Redirect("/Home/Index");
                }
                else
                {
                    ViewBag.message = "密码错误！";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "不存在该用户！";
                return View();
            }
        }

    }
}
