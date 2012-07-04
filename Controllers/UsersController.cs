using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevenMouths.Models;
using SevenMouths.Helpers;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            string emailOrName = collection["emailOrName"];
            if (string.IsNullOrEmpty(emailOrName))
            {
                ViewBag.message = "邮箱或用户名不允许为空！";
                return View();
            }
            else
            {
                if (context.Users.Count(x => x.Name == emailOrName || x.Email == emailOrName)>0)
                {
                    ViewBag.message = "已存在该邮箱或用户名！";
                    return View();
                }

            }
            string password = collection["password"];
            string pwdConfirm = collection["pwdConfirm"];
            if (password.Length < 6 || pwdConfirm.Length < 6)
            {
                ViewBag.message = "密码不允许小于6位！";
                return View();
            }
            else
            {
                if (password != pwdConfirm)
                {
                    ViewBag.message = "两次输入的密码不一样！";
                    return View();
                }
                else
                {
                    User user = new User();
                    if (emailOrName.IndexOf("@") > 0)//输入的是邮箱
                    {
                        if (!CommomHelper.CheckEmail(emailOrName))
                        {
                            ViewBag.message = "输入邮箱格式不正确！";
                            return View();
                        }
                        else
                        {
                            user.UserNum = CommomHelper.GetRandomNum();
                            user.Email = emailOrName;
                            user.Password = password;
                            context.Users.AddObject(user);
                            context.SaveChanges();

                            //保存或更新cookie
                            cookie["UserId", true, false] = user.UserId+"";
                            return Redirect("/Home/Index");
                        }
                    }
                    else//输入的是用户名
                    {
                        user.Name = emailOrName;
                        user.Password = password;
                        context.Users.AddObject(user);
                        context.SaveChanges();

                        //保存或更新cookie
                        cookie["UserId", true, false] = user.UserId + "";
                        return Redirect("/Home/Index");
                    }
                    
                }
            }
        }

        public ActionResult Detail(int? id)//id=userId
        {
            User user = context.Users.FirstOrDefault(x => x.UserId == id);
            ViewBag.user = user;
            List<Share> myShares = null;
            if(user != null)
            {
                myShares = context.Shares.Where(x => x.SharedBy == id).ToList();
            }

            return View(myShares);
        }

        //注销
        public ActionResult LogOut()
        {
            cookie["UserId", true, false] = 0+"";
            return Redirect("/Users/Login");
        }

        #region 热门网站接入


        #region QQ空间 接入

        public ActionResult LoginQZone()
        {
            string key = ConfigurationManager.AppSettings["ConsumerKey"];
            string secret = ConfigurationManager.AppSettings["ConsumerSecret"];

            var context = new QzoneSDK.Context.QzoneContext(key, secret);
            string callbackUrl = ConfigurationManager.AppSettings["CallbackUrl"];
            var requestTokey = context.GetRequestToken(callbackUrl);
            Session["requestTokenKey"] = requestTokey.TokenKey;
            Session["requestTokeySecret"] = requestTokey.TokenSecret;

            var authenticationUrl = context.GetAuthorizationUrl(requestTokey, callbackUrl);
            return Redirect(authenticationUrl);
        }

        public ActionResult OnLoginQZone()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OnLoginQZone(FormCollection collection)
        {
            if (Request.QueryString["oauth_vericode"] != null)
            {
                var requestTokenKey = Session["requestTokenKey"].ToString();
                var requestTokenSecret = Session["requestTokenSecret"].ToString();
                var verifier = Request.QueryString["oauth_vericode"];
                string key = ConfigurationManager.AppSettings["ConsumerKey"];
                string secret = ConfigurationManager.AppSettings["ConsumerSecret"];
                QzoneSDK.Qzone qzone = new QzoneSDK.Qzone(key, secret, requestTokenKey, requestTokenSecret, verifier);
                var access = qzone.OAuthTokenKey;

                var currentUser = qzone.GetCurrentUser();
                //var user = (QzoneSDK.Models.BasicProfile)JsonConvert(typeof(QzoneSDK.Models.BasicProfile), currentUser);
            }

            return View();
        }

        #endregion

        #endregion


        #region 辅助方法

        //判断邮箱或用户名是否重名，重名的话不以注册
        public ActionResult CheckEmailOrName(FormCollection collection)
        {
            bool isReduplicate = false;
            string message = "";
            string emailOrName = collection["emailOrName"];
            if(!string.IsNullOrEmpty(emailOrName))
            {
                if (context.Users.Count(x => x.Name == emailOrName || x.Email == emailOrName) > 0)
                {
                    isReduplicate = true;
                    message = "已经存在此邮箱或用户名！";
                }
            }

            var dataReturn = new { isReduplicate=isReduplicate,message=message};
            return Json(dataReturn);

        }

        #endregion
    }
}
