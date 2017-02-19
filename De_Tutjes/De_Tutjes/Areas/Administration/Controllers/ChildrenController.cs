using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace De_Tutjes.Areas.Administration.Controllers
{
    public class ChildrenController : Controller
    {
        // GET: Administration/Children
        public ActionResult Overview()
        {
            return View();
        }

        [Route("{step}")]
        // GET: Administration/Children/Create
        public ActionResult Create(int? step)
        {
            if (step == null)
            {
                step = 1;
            }
            ViewBag.FormStep = step;
            return View();
        }
    }
}