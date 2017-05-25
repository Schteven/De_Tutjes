using De_Tutjes.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace De_Tutjes.Areas.Administration.Controllers
{
    public class SharedController : Controller
    {
        // GET: Administration/Shared
        public ActionResult Index()
        {
            return View();
        }

        // Shared JQuery AJAX Functions

        [HttpPost]
        public JsonResult GetPostalCode(string input)
        {
            AutocompleteAddress aca = new AutocompleteAddress();
            aca.Initialize();

            SortedList<string, string> postalCodes = aca.GetPostalcodes();

            var PostalCode = postalCodes.Where(pc => pc.Key.StartsWith(input))
                                        .Distinct()
                                        .Select(pc => new { id = pc.Key, str = pc.Value })
                                        .OrderBy(pc => pc);
            return Json(PostalCode, JsonRequestBehavior.AllowGet);

        }
    }
}