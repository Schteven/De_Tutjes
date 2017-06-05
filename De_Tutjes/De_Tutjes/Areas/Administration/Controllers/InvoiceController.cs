using De_Tutjes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace De_Tutjes.Areas.Administration.Controllers
{
    public class InvoiceController : Controller
    {
        InvoiceService Is = new InvoiceService();
        // GET: Administration/Invoice
        public ActionResult Overview()
        {
            



            return View();
        }
    }
}