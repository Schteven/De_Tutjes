using De_Tutjes.Functions;
using Microsoft.Ajax.Utilities;
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
        public JsonResult GetPostalCodes(string input)
        {
            AutocompleteAddress aca = new AutocompleteAddress();
            aca.Initialize();

            List<XMLAddress> addresses = aca.XmlData.addressList;

            var postalCode = addresses.Where(pc => pc.Postalcode.StartsWith(input)).DistinctBy(pc => pc.Postalcode);
            return Json(postalCode, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetCity(string input)
        {
            AutocompleteAddress aca = new AutocompleteAddress();
            aca.Initialize();

            List<XMLAddress> addresses = aca.XmlData.addressList.Where(pc => pc.Postalcode.Equals(input)).ToList();

            XMLAddress address = addresses.Where(pc => pc.Postalcode.Equals(input)).FirstOrDefault();
            string city = address.Municipal;
            return Json(city, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetStreet(string postal, string input)
        {
            AutocompleteAddress aca = new AutocompleteAddress();
            aca.Initialize();

            List<XMLAddress> addresses = aca.XmlData.addressList.Where(pc => pc.Postalcode.Equals(postal)).ToList();

            var addressList = addresses.Where(pc => pc.Street.StartsWith(input));
            return Json(addressList, JsonRequestBehavior.AllowGet);

        }

    }
}