﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevenMouths.Models;
using SevenMouths.Helpers;

namespace SevenMouths.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            List<Share> shares = context.Shares.ToList();

            return View(shares);
        }

        //获取所有视频分享
        [HttpPost]
        public ActionResult GetVedios()
        { 
            int categoryId = context.Categories.FirstOrDefault(x => x.Name == "vedio").CategoryId;
            List<Share> vedios = context.Shares.Where(x => x.CategoryId == categoryId).ToList();

            return View(vedios);
        }

        //获取所有书分享
        [HttpPost]
        public ActionResult GetBooks()
        {
            int categoryId = context.Categories.FirstOrDefault(x => x.Name == "book").CategoryId;
            List<Share> books = context.Shares.Where(x => x.CategoryId == categoryId).ToList();

            return View(books);
        }

    }
}
