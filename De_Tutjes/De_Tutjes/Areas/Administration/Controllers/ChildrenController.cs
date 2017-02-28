using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using De_Tutjes.Models;
using De_Tutjes.Areas.Administration.Models;

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

        [Route("{step}")]
        //POST: Administration/Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? step, CreateChildAndParentsModel model)
        {
            if (step == 1) {
                // Child
                Toddler toddler = new Toddler();
                toddler.Person.FirstName = model.childFirstname;
                toddler.Person.LastName = model.childLastname;
                toddler.Person.Sex = model.childSex;
                toddler.Person.BirthDate = model.childBirthdate;
                toddler.Person.RegistrationDate = DateTime.Now;

                // First Parent
                Parent parent = new Parent();
                parent.Person.FirstName = model.firstParentFirstname;
                parent.Person.LastName = model.firstParentLastname;
                parent.Person.BirthDate = model.firstParentBirthdate;
                parent.Person.RegistrationDate = DateTime.Now;
                
            }
            return View();
        }
        
    }
}