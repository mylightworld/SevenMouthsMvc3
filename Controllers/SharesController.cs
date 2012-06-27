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
        //
        // GET: /Shares/

        public ActionResult Index()
        {
            return View();
        }

        //顶
        [HttpPost]
        public ActionResult VoteToUp(int shareId)
        {
            Share share = context.Shares.FirstOrDefault(x => x.ShareId == shareId);
            if (share != null)
            {
                Vote vote = new Vote();
                vote.ShareId = shareId;
                vote.Value = 1;
                vote.VotedBy = cookie.UserId;
                vote.VotedAt = System.DateTime.Now;
                context.Votes.AddObject(vote);
                context.SaveChanges();
            }

            return RedirectToAction("/Home/Index");
        }

        //踩
        [HttpPost]
        public ActionResult VoteToDown(int shareId)
        {
            Share share = context.Shares.FirstOrDefault(x => x.ShareId == shareId);
            if (share != null)
            {
                Vote vote = new Vote();
                vote.ShareId = shareId;
                vote.Value = -1;
                vote.VotedBy = cookie.UserId;
                vote.VotedAt = System.DateTime.Now;
                context.Votes.AddObject(vote);
                context.SaveChanges();
            }

            return RedirectToAction("/Home/Index");
        }

    }
}
