using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace De_Tutjes.Areas.Administration.Controllers
{
    public class UserController : Controller
    {
        // GET: Administration/Account
        public ActionResult Login()
        {
            return View();
        }
    }
}