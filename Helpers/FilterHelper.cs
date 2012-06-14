using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevenMouths.Helpers;

namespace SevenMouths.Helpers
{
    public class FilterHelper
    {
    }

    //对是否登录进行过滤
    public class LoginFilter : FilterAttribute,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            CookieHelper cookie = new CookieHelper();
            if (cookie.IsLogin)
            {
                filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CookieHelper cookie = new CookieHelper();
            if (cookie.IsLogin)
            {
                filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }
    }

   
}