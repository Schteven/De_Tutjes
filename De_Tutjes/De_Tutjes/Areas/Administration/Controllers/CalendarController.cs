using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace De_Tutjes.Areas.Administration.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Administration/Calendar
        public ActionResult Overview()
        {
            return View();
        }
    }
}