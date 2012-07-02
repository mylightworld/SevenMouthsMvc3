using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SevenMouths.Helpers
{
    public class CookieHelper
    {
        private static string cookiePrefix = "SevenMouths_"; 

        public static void SaveCookie(string key,string value,bool isSave,bool isExpire)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookiePrefix+key);
            if (cookie == null)
            {
                cookie = new HttpCookie(cookiePrefix + key);
                cookie.Value = HttpContext.Current.Server.HtmlEncode(value); 
            }
            else
            { 
                cookie.Value = HttpContext.Current.Server.HtmlEncode(value);
            }

            if (isSave && !isExpire)
                    cookie.Expires = DateTime.Now.AddYears(1);
            if (!isSave && isExpire)
                    cookie.Expires = DateTime.Now.AddYears(-1);

            HttpContext.Current.Response.Cookies.Add(cookie);
            HttpContext.Current.Items[key] = value;

        }

        public static string GetCookie(string key)
        {
            if (HttpContext.Current.Items[key] != null)
            {
                return HttpContext.Current.Items[key].ToString();
            }

            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiePrefix + key];
            if ( cookie != null)
                return HttpContext.Current.Server.HtmlDecode(cookie.Value);
            else
                return "";

        }

        public string this[string key,bool isSave,bool isExpire]
        {
            set
            {
                CookieHelper.SaveCookie(key,value,isSave,isExpire);
            }
            
        }

        public string this[string key]
        {
            get
            {
                return GetCookie(key);
            }
        }

        public int UserId
        {
            get
            {
                if (!string.IsNullOrEmpty(this["UserId"]))
                {
                    return int.Parse(this["UserId"]);
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