using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using De_Tutjes.Models;
using De_Tutjes.Areas.Administration.Models;
using Newtonsoft.Json;
using System.Data.Entity;

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

        // GET: Administration/Children/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Administration/Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id)
        {
            return View(id);
        }

        /**TODDLER********************************/
        // GET: Administration/Children/_AddToddler
        public PartialViewResult _AddToddler()
        {
            CreateToddlerOverview cto = new CreateToddlerOverview();
            cto.toddlers = db.Toddlers
                .Include(i => i.Person)
                .ToList();
            cto.toddler = new Toddler();
            return PartialView(cto);
        }

        //POST: Administration/Children/_AddToddler
        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult _AddToddler(CreateToddlerOverview model)
        {
            if (ModelState.IsValid)
            {
                return PartialView("_AddToddler");
            }
            return PartialView(model);
        }
        [HttpPost]
        public PartialViewResult CreateToddler(CreateToddlerOverview model)
        {
            if (ModelState.IsValid)
            {
                Toddler toddler = new Toddler();
           
                toddler.Person = new Person();
                toddler.Person.FirstName = model.toddler.Person.FirstName;
                toddler.Person.LastName = model.toddler.Person.LastName;
                toddler.Person.Gender = model.toddler.Person.Gender;
                toddler.Person.BirthDate = model.toddler.Person.BirthDate;
                toddler.Person.RegistrationDate = DateTime.Now;
                toddler.Person.Active = true;

                db.Toddlers.Add(toddler);
                db.SaveChanges();

                RelationLink relationLink = new RelationLink();
                relationLink.RelationToChild = "isChild";
                relationLink.Toddler = db.Toddlers.Find(toddler.ToddlerId);
                relationLink.Person = db.Persons.Find(toddler.Person.PersonId);

                db.RelationLinks.Add(relationLink);
                db.SaveChanges();

                Session["ToddlerId"] = db.Toddlers.Find(toddler.ToddlerId).ToddlerId;

            }
            return PartialView("_ListToddler", db.Toddlers.Include(i => i.Person).ToList());
        }

        /**PARENTS****************************/
        // GET: Administration/Children/Parents
        public PartialViewResult _AddParents()
        {
            int id = 0;
            CreateParentsOverview cvo = new CreateParentsOverview();
            cvo.relationLinks = db.RelationLinks
                .Include(i => i.Person)
                .Include(i => i.Person.Address)
                .Include(i => i.Person.ContactDetail)
                .Where(i => (i.ToddlerId == id) && (i.RelationToChild == "isParent"))
                .ToList();

            cvo.parents = db.Parents
                .Include(i => i.Person)
                .Include(i => i.Person.Address)
                .Include(i => i.Person.ContactDetail)
                .ToList();

            cvo.parent = new Parent();
            return PartialView(cvo);
        }

        //POST: Administration/Children/Parents
        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult _AddParents(CreateParentsOverview model)
        {
            if (ModelState.IsValid)
            {
                return PartialView("_AddChild");
            }
            return PartialView(model);
        }
        
        [HttpPost]
        public PartialViewResult CreateParent(CreateParentsOverview model)
        {
            if (ModelState.IsValid)
            {
                Parent parent = new Parent();

                parent.Person = new Person();
                parent.Person.FirstName = model.parent.Person.FirstName;
                parent.Person.LastName = model.parent.Person.LastName;
                parent.Person.Gender = model.parent.Person.Gender;
                parent.Person.BirthDate = model.parent.Person.BirthDate;
                parent.Person.RegistrationDate = DateTime.Now;
                parent.Person.Active = true;
                
                parent.Person.Address = new Address();
                parent.Person.Address.City = model.parent.Person.Address.City;
                parent.Person.Address.PostalCode = model.parent.Person.Address.PostalCode;
                parent.Person.Address.Street = model.parent.Person.Address.Street;
                parent.Person.Address.Number = model.parent.Person.Address.Number;
                parent.Person.Address.Bus = model.parent.Person.Address.Bus;

                parent.Person.ContactDetail = new ContactDetail();
                parent.Person.ContactDetail.HomePhone = model.parent.Person.ContactDetail.HomePhone;
                parent.Person.ContactDetail.CellPhone = model.parent.Person.ContactDetail.CellPhone;
                parent.Person.ContactDetail.WorkPhone = model.parent.Person.ContactDetail.WorkPhone;
                parent.Person.ContactDetail.Email = model.parent.Person.ContactDetail.Email;
                
                db.Parents.Add(parent);
                db.SaveChanges();
            }
            return PartialView("_ListParents",
                            db.Parents.Include(i => i.Person).Include(i => i.Person.Address).Include(i => i.Person.ContactDetail).ToList()
            );
        }

    }
}