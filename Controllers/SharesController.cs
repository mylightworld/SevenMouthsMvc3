using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevenMouths.Models;

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

    }
}
