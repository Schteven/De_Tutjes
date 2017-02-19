using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace De_Tutjes.Areas.Administration.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Administration/Settings
        public ActionResult Overview()
        {
            return View();
        }
    }
}