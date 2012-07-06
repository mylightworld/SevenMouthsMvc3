using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevenMouths.Models;
using SevenMouths.Helpers;

namespace SevenMouths.Controllers
{
    public class LogoVotesController : BaseController
    {
        public ActionResult Index()
        {
            if(!cookie.IsLogin)
            {
                return Redirect("/Users/Login");    
            }

            List<Logo> logoVotes = context.Logos.ToList();
            return View(logoVotes);
        }

        //顶
        public ActionResult VoteToUp(int id)//id为logoId
        {
            Logo logo = context.Logos.FirstOrDefault(x => x.LogoId == id);
            if (logo != null)
            {
                LogoVote logoVote = new LogoVote();
                logoVote.LogoId = id;
                logoVote.Value = 1;
                logoVote.Comment = Request.Params["comment"];
                logoVote.VotedBy = cookie.UserId;
                logoVote.VoteAt = DateTime.Now;

                context.LogoVotes.AddObject(logoVote);
                context.SaveChanges();
            }

            var dataReturn = new { upCounts = context.LogoVotes.Count(x => x.LogoId == id &&x.Value > 0) };
            return Json(dataReturn);
        }

        //踩
        public ActionResult VoteToDown(int id)//id为shareId
        {
            Logo logo = context.Logos.FirstOrDefault(x => x.LogoId == id);
            if (logo != null)
            {
                LogoVote logoVote = new LogoVote();
                logoVote.LogoId = id;
                logoVote.Value = -1;
                logoVote.Comment = Request.Params["comment"];
                logoVote.VotedBy = cookie.UserId;
                logoVote.VoteAt = DateTime.Now;

                context.LogoVotes.AddObject(logoVote);
                context.SaveChanges();
            }

            var dataReturn = new { downCounts = context.LogoVotes.Count(x => x.LogoId == id && x.Value < 0) };
            return Json(dataReturn);
        }
    }
}
