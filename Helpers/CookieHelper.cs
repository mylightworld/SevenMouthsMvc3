using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SevenMouths.Helpers
{
    public class CookieHelper
    {
        private static string cookiePrefix = "SevenMouths_"; 
        private HttpCookie cookie;

        public CookieHelper()
        {
            cookie = System.Web.HttpContext.Current.Request.Cookies[cookiePrefix];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookiePrefix);
            }
        }

        public string this[string key,bool isSave,bool isExpire]
        {
            set
            {
                cookie[cookiePrefix + key] = value;

                if (isSave && !isExpire)
                    cookie.Expires = DateTime.Now.AddYears(1);
                if (!isSave && isExpire)
                    cookie.Expires = DateTime.Now.AddYears(-1);
            }
            get
            {
                return cookie[cookiePrefix + key];
            }
        }

        public int UserId
        {
            get
            {
                if (!string.IsNullOrEmpty(cookie[cookie + "UserId"]))
                {
                    return int.Parse(cookie[cookie + "UserId"]);
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool IsLogin
        {
            get
            {
                if (UserId != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}