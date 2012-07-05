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
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CookieHelper cookie = new CookieHelper();
            if (cookie.IsLogin)
            {
                //filterContext.Result = new RedirectResult("/Home/Index");
                filterContext.Result = new RedirectResult("/LogoVotes/Index");
                
            }
            //else
            //{
            //    filterContext.Result = new RedirectResult("/Users/Login");
            //}
        }
    }

   
}