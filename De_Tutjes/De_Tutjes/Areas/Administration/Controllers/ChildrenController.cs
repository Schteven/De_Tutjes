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
        private DeTutjesContext db = new DeTutjesContext();


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

        //POST: Administration/Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewChildModel model)
        {
            if (ModelState.IsValid)
            {
                // Child
                Toddler toddler = new Toddler();
                toddler.Person = new Person();
                toddler.Person.FirstName = model.childFirstname;
                toddler.Person.LastName = model.childLastname;
                toddler.Person.Gender = model.childGender;
                toddler.Person.BirthDate = model.childBirthdate;
                toddler.Person.RegistrationDate = DateTime.Now;
                toddler.Person.Active = true;

                db.Toddlers.Add(toddler);
                db.SaveChanges();

                // First Parent
                Parent firstParent = new Parent();
                if (model.firstParentGender == "m")
                {
                    firstParent.Relation = "Vader";
                }
                else
                {
                    firstParent.Relation = "Moeder";
                }
                firstParent.Person = new Person();
                firstParent.Person.FirstName = model.firstParentFirstname;
                firstParent.Person.LastName = model.firstParentLastname;
                firstParent.Person.BirthDate = model.firstParentBirthdate;
                firstParent.Person.Gender = model.firstParentGender;
                firstParent.Person.RegistrationDate = DateTime.Now;
                firstParent.Person.Active = true;

                firstParent.Person.Address = new Address();
                firstParent.Person.Address.Street = model.firstParentStreet;
                firstParent.Person.Address.Number = model.firstParentHouseNumber;
                firstParent.Person.Address.Bus = model.firstParentHouseBus;
                firstParent.Person.Address.PostalCode = model.firstParentPostalCode;
                firstParent.Person.Address.City = model.firstParentCity;

                firstParent.Person.ContactDetail = new ContactDetail();
                firstParent.Person.ContactDetail.HomePhone = model.firstParentPhoneHome;
                firstParent.Person.ContactDetail.CellPhone = model.firstParentPhoneMobile;
                firstParent.Person.ContactDetail.WorkPhone = model.firstParentPhoneWork;
                firstParent.Person.ContactDetail.Email = model.firstParentEmailAddress;

                db.Parents.Add(firstParent);
                db.SaveChanges();

                // Second Parent
                Parent secondParent = new Parent();
                if (model.secondParentGender == "m")
                {
                    secondParent.Relation = "Vader";
                }
                else
                {
                    secondParent.Relation = "Moeder";
                }
                secondParent.Person = new Person();
                secondParent.Person.FirstName = model.secondParentFirstname;
                secondParent.Person.LastName = model.secondParentLastname;
                secondParent.Person.BirthDate = model.secondParentBirthdate;
                secondParent.Person.Gender = model.secondParentGender;
                secondParent.Person.RegistrationDate = DateTime.Now;
                secondParent.Person.Active = true;


                if (model.isLivingTogether)
                {
                    Address address = db.Addresses.Find(firstParent.Person.AddressId);
                    secondParent.Person.AddressId = address.AddressId;
                }
                else
                {
                    secondParent.Person.Address = new Address();
                    secondParent.Person.Address.Street = model.secondParentStreet;
                    secondParent.Person.Address.Number = model.secondParentHouseNumber;
                    secondParent.Person.Address.Bus = model.secondParentHouseBus;
                    secondParent.Person.Address.PostalCode = model.secondParentPostalCode;
                    secondParent.Person.Address.City = model.secondParentCity;
                }
                
                secondParent.Person.ContactDetail = new ContactDetail();
                secondParent.Person.ContactDetail.HomePhone = model.secondParentPhoneHome;
                secondParent.Person.ContactDetail.CellPhone = model.secondParentPhoneMobile;
                secondParent.Person.ContactDetail.WorkPhone = model.secondParentPhoneWork;
                secondParent.Person.ContactDetail.Email = model.secondParentEmailAddress;

                db.Parents.Add(secondParent);
                db.SaveChanges();

                return View("~/Areas/Administration/Children/Create/#b");
                }
            return View(model);
        }

    }
}