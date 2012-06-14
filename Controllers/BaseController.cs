using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevenMouths.Helpers;
using SevenMouths.Models;

namespace SevenMouths.Controllers
{
    public class BaseController : Controller
    {
        protected CookieHelper cookie = new CookieHelper();
        protected SevenMouthsEntities context = new SevenMouthsEntities();

    }
}
