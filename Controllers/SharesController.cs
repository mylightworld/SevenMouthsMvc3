using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevenMouths.Models;
using SevenMouths.Helpers;

namespace SevenMouths.Controllers
{
    public class SharesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        //顶
        public ActionResult VoteToUp(int id)//id为shareId
        {
            Share share = context.Shares.FirstOrDefault(x => x.ShareId == id);
            if (share != null)
            {
                Vote vote = new Vote();
                vote.ShareId = id;
                vote.Value = 1;
                vote.VotedBy = cookie.UserId;
                vote.VotedAt = System.DateTime.Now;
                context.Votes.AddObject(vote);
                context.SaveChanges();
            }

            var dataReturn = new { upCounts = context.Votes.Count(x => x.Value > 0) };
            return Json(dataReturn);
        }

        //踩
        public ActionResult VoteToDown(int id)//id为shareId
        {
            Share share = context.Shares.FirstOrDefault(x => x.ShareId == id);
            if (share != null)
            {
                Vote vote = new Vote();
                vote.ShareId = id;
                vote.Value = -1;
                vote.VotedBy = cookie.UserId;
                vote.VotedAt = System.DateTime.Now;
                context.Votes.AddObject(vote);
                context.SaveChanges();
            }

            var dataReturn = new { downCounts = context.Votes.Count(x => x.Value <0) };
            return Json(dataReturn);
        }

        //提交评论
        [HttpPost]
        public ActionResult SubmitComment(FormCollection collection)
        { 
            int shareId = int.Parse(collection["shareId"]);
            int parentId = int.Parse(collection["parentId"]);

            Comment commnet = new Comment();
            commnet.ShareId = shareId;
            commnet.ParentId = parentId;
            commnet.Description = collection["commentContent"];
            commnet.IsOriginal = false;
            commnet.CommentedBy = cookie.UserId;
            commnet.CommentedAt = DateTime.Now;

            //调试
            try
            {

                context.Comments.AddObject(commnet);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e.InnerException);
            }

            var share = context.Shares.FirstOrDefault(x => x.ShareId == shareId);
            return View("_Comments",share);
        }

        public ActionResult Add()
        {
            ViewBag.categories = context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(FormCollection collection)
        {
            string title = collection["title"];
            int categoryId = int.Parse(collection["category"]);
            string description = collection["description"];
            string url = collection["url"];
            string reason = collection["reason"];
            string score = collection["score"];

            //添加一个分享
            Share share = new Share();
            share.ShareNum = CommomHelper.GetRandomNum();
            share.Title = title;
            share.CategoryId = categoryId;
            share.Description = description;
            share.Url = url;
            share.SharedBy = cookie.UserId;
            share.SharedAt = DateTime.Now;

            context.Shares.AddObject(share);
            context.SaveChanges();

            //添加分享理由，即原始评论
            Comment comment = new Comment();
            comment.ShareId = share.ShareId;
            comment.Grade = int.Parse(score);
            comment.Description = reason;
            comment.IsOriginal = true;
            comment.CommentedBy = cookie.UserId;
            comment.CommentedAt = DateTime.Now;
            context.Comments.AddObject(comment);
            context.SaveChanges();

            return Redirect("/Home/Index");
        }
    }
}
