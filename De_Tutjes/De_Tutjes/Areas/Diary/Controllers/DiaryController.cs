using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace De_Tutjes.Areas.Diary.Controllers
{
    public class DiaryController : Controller
    {
        // GET: Diary/Diary
        public ActionResult Index()
        {
            return View();
        }
    }
}