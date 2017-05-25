using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Globalization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using System.Net.Mail;

using De_Tutjes.Models;
using De_Tutjes.Functions;
using De_Tutjes.Areas.Administration.Models;

namespace De_Tutjes.Areas.Administration.Controllers
{
    public class ChildrenController : Controller
    {
        private DateTime CreateAgreedDays_StartDate;
        private DateTime CreateAgreedDays_EndDate;
        NewChildWizardSession ncws;

        private DeTutjesContext db = new DeTutjesContext();

        // GET: Administration/Children

        public ActionResult Overview()
        {
            ToddlersOverview to = new ToddlersOverview();
            to.toddler = new Toddler();
            to.toddlerList = db.Toddlers
                .Where(a => a.Person.Active == true)
                .Include(p => p.Person)
                .Include(f => f.Food)
                .Include(s => s.Sleep)
                .Include(m => m.Medical)
                .ToList();
            return View(to);
        }

        [HttpPost]
        public PartialViewResult ShowToddler(string toddlerOverview)
        {
            Toddler toddler;
            int id = 0;
            if (int.TryParse(toddlerOverview, out id))
            {
                id = Int32.Parse(toddlerOverview);
            }
            if (id != 0)
            {
                toddler = db.Toddlers
                    .Where(i => i.ToddlerId.Equals(id))
                    .Include(p => p.Person)
                    .Include(f => f.Food)
                    .Include(s => s.Sleep)
                    .Include(m => m.Medical)
                    .Include(d => d.Medical.Doctor)
                    .Include(dp => dp.Medical.Doctor.Person)
                    .Include(dpa => dpa.Medical.Doctor.Person.Address)
                    .Include(dpc => dpc.Medical.Doctor.Person.ContactDetail)
                    .FirstOrDefault();

                ViewBag.Parents = GetParentsOfToddler(toddler);
                ViewBag.Pickups = GetPickupsOfToddler(toddler);
                ViewBag.AgreedDays = GetAgreedDaysOfToddler(toddler);
            } else
            {
                toddler = new Toddler();
            }
            return PartialView("_OverviewShowToddler", toddler);
        }

        // GET: Administration/Children/Create
        public ActionResult Create()
        {
            string username = "demo";
            //ncws = db.NewChildWizardSessions.Where(u => u.Username.Equals(username)).Where(s => s.Stop == null).FirstOrDefault();
            //if (ncws == null)
            //{

                Guid sessionKey = Guid.NewGuid();
                string key = sessionKey.ToString();

                ncws = new NewChildWizardSession();

                ncws.Username = username;
                ncws.ToddlerSession = key;
                ncws.Start = DateTime.Now;
                ncws.Complete = false;

                Session["NewChildWizardSession"] = key;
                ViewBag.Session = key;

            /*}
            else
            {
                Session["NewChildWizardSession"] = ncws.ToddlerSession;
                ViewBag.NotFinished = ncws.ToddlerSession;
                ViewBag.SessionStarted = ncws.Start;
                ViewBag.Session = ncws.ToddlerSession;
            }*/

            return View();
        }
        //POST: Administration/Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id)
        {
            Toddler toddler = GetCurrentToddler();
            ICollection<Parent> parents = GetParentsOfCurrentToddler();

            toddler.Person.Active = true;

            db.Entry(toddler).State = EntityState.Modified;

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            string emailIsDouble = "";

            foreach (Parent parent in parents)
            {
                string email = parent.Person.ContactDetail.Email;
                parent.Person.Active = true;

                if (email != emailIsDouble)
                {

                    parent.Person.UserAccount = new ApplicationUser();
                    parent.Person.UserAccount.UserName = email;
                    parent.Person.UserAccount.Email = email;

                    string userPWD = Membership.GeneratePassword(5, 1);

                    var chkUser = UserManager.Create(parent.Person.UserAccount, userPWD);

                    SendFirstMail(email, userPWD);
                }
                else
                {
                    parent.Person.UserAccountId = db.Users.Where(e => e.Email == emailIsDouble).FirstOrDefault().Id;
                }

                db.Entry(parent).State = EntityState.Modified;

                emailIsDouble = email;
            }

            //NewChildWizardSession ncws = db.NewChildWizardSessions.Where(s => s.ToddlerSession == toddler.ToddlerSession).FirstOrDefault();

            //ncws.Stop = DateTime.Now;
            //ncws.Complete = true;
            //db.Entry(ncws).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Overview", "Children");
        }

        /**TODDLER********************************/
        // GET: Administration/Children/_AddToddler
        public PartialViewResult _AddToddler()
        {
            Toddler toddler = GetCurrentToddler();
            CreateToddlerOverview cto = new CreateToddlerOverview();
            if (toddler != null)
            {
                cto.toddler = toddler;
            }
            else
            {
                cto.toddler = new Toddler();
            }
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
                toddler.ToddlerSession = GetNewChildWizardSession();

                toddler.Person = new Person();
                toddler.Person.FirstName = model.toddler.Person.FirstName;
                toddler.Person.LastName = model.toddler.Person.LastName;
                toddler.Person.Gender = model.toddler.Person.Gender;
                toddler.Person.BirthDate = model.toddler.Person.BirthDate;
                toddler.Person.RegistrationDate = DateTime.Now;
                toddler.Person.Active = false;

                db.Toddlers.Add(toddler);
                db.SaveChanges();

                RelationLink relationLink = new RelationLink();
                relationLink.RelationToChild = "isChild";
                relationLink.Toddler = db.Toddlers.Find(toddler.ToddlerId);
                relationLink.Person = db.Persons.Find(toddler.Person.PersonId);

                db.RelationLinks.Add(relationLink);
                db.SaveChanges();

                //db.NewChildWizardSessions.Add(ncws);
                //db.SaveChanges();

                return PartialView("_ListToddler", toddler);
            }
            else
            {
                Toddler current = GetCurrentToddler();
                return PartialView("_ListToddler", current);
            }
        }

        /**PARENTS****************************/
        // GET: Administration/Children/Parents
        public PartialViewResult _AddParents()
        {
            CreateParentsOverview cvo = new CreateParentsOverview();
            cvo.relationLinks = GetRelationLinksOfCurrentToddler(); 
            cvo.parents = GetParentsOfCurrentToddler();
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
                Toddler toddler = GetCurrentToddler();
                Parent parent = new Parent();

                parent.Person = new Person();
                parent.Person.FirstName = model.parent.Person.FirstName;
                parent.Person.LastName = model.parent.Person.LastName;
                parent.Person.Gender = model.parent.Person.Gender;
                parent.Person.BirthDate = model.parent.Person.BirthDate;
                parent.Person.RegistrationDate = DateTime.Now;
                parent.Person.Active = false;
                
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

                RelationLink relationLink = new RelationLink();
                relationLink.Person = db.Persons.Find(parent.PersonId);
                relationLink.Toddler = toddler;
                relationLink.RelationToChild = "isParent";

                db.RelationLinks.Add(relationLink);
                db.SaveChanges();

            }
            return PartialView("_ListParents", GetParentsOfCurrentToddler());
        }

        /**AGREEDDAYSANDPICKUPS***************/
        // GET: Administration/Children/AgreedDaysAndPickups
        public PartialViewResult _AddAgreedDaysAndPickups()
        {
            Toddler toddler = GetCurrentToddler();
            string readyForDaycare = "";
            string readyForSchool = "";
            if (toddler != null)
            {
                DateTime birthdate = toddler.Person.BirthDate;

                if (birthdate != null)
                {
                    readyForDaycare = CalculateReadyForDaycare(birthdate);
                    readyForSchool = CalculateReadyForSchool(birthdate);
                }
            }
            CreateAgreedDaysAndPickup caap = new CreateAgreedDaysAndPickup();
            caap.relationLinks = GetRelationLinksOfCurrentToddler();
            caap.agreedDaysList = GetAgreedDaysOfCurrentToddler();
            caap.pickups = GetPickupsOfCurrentToddler();
            caap.agreedDays = new AgreedDays();
            caap.pickup = new Pickup();

            ViewBag.ReadyForDaycare = readyForDaycare;
            ViewBag.ReadyForSchool = readyForSchool;
            return PartialView(caap);
        }
        //POST: Administration/Children/AgreedDaysAndPickups
        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult _AddAgreedDaysAndPickups(CreateAgreedDaysAndPickup model)
        {
            if (ModelState.IsValid)
            {
                return PartialView("_AddAgreedDaysAndPickups");
            }
            return PartialView(model);
        }
        [HttpPost]
        public PartialViewResult CreateAgreedDays(CreateAgreedDaysAndPickup model)
        {
            string session = GetNewChildWizardSession();
            if (ModelState.IsValid)
            {
                Toddler toddler = GetCurrentToddler();
                AgreedDays agreedDays = new AgreedDays();

                agreedDays.ToddlerId = toddler.ToddlerId;

                agreedDays.StartDate = model.agreedDays.StartDate;
                agreedDays.EndDate = model.agreedDays.EndDate;

                agreedDays.Monday = (bool) model.agreedDays.Monday;
                agreedDays.Tuesday = (bool) model.agreedDays.Tuesday;
                agreedDays.Wednesday = (bool) model.agreedDays.Wednesday;
                agreedDays.Thursday = (bool) model.agreedDays.Thursday;
                agreedDays.Friday = (bool) model.agreedDays.Friday;

                agreedDays.SpecialNotice = model.agreedDays.SpecialNotice;

                db.AgreedDays.Add(agreedDays);
                db.SaveChanges();

            }

            return PartialView("_ListAgreedDays", GetAgreedDaysOfCurrentToddler());
        }
        [HttpPost]
        public PartialViewResult CreatePickup(CreateAgreedDaysAndPickup model)
        {
            string session = GetNewChildWizardSession();
            if (ModelState.IsValid)
            {
                Toddler toddler = GetCurrentToddler();

                Pickup pickup = new Pickup();
                pickup.Relation = model.pickup.Relation;

                pickup.Person = new Person();
                pickup.Person.FirstName = model.pickup.Person.FirstName;
                pickup.Person.LastName = model.pickup.Person.LastName;
                pickup.Person.BirthDate = DateTime.Now;
                pickup.Person.RegistrationDate = DateTime.Now;
                pickup.Person.Active = false;

                pickup.Person.Address = new Address();
                pickup.Person.Address.City = model.pickup.Person.Address.City;
                pickup.Person.Address.PostalCode = model.pickup.Person.Address.PostalCode;

                pickup.Person.ContactDetail = new ContactDetail();
                pickup.Person.ContactDetail.CellPhone = model.pickup.Person.ContactDetail.CellPhone;

                db.Pickups.Add(pickup);
                db.SaveChanges();

                RelationLink relationLink = new RelationLink();

                relationLink.RelationToChild = "isPickup";
                relationLink.Person = db.Persons.Find(pickup.PersonId);
                relationLink.Toddler = toddler;

                db.RelationLinks.Add(relationLink);
                db.SaveChanges();
                
            }
            return PartialView("_ListPickups", GetPickupsOfCurrentToddler());
        }
        
        /**MEDICALINFORMATION***************/
        // GET: Administration/Children/MedicalInformation
        public PartialViewResult _AddMedicalInformation()
        {

            Toddler toddler = GetCurrentToddler();

            CreateMedicalInformation cmi = new CreateMedicalInformation();
            if (toddler != null)
            {
                cmi.medicalInfo = toddler.Medical;
            }
            cmi.medical = new Medical();

            return PartialView(cmi);

        }
        //POST: Administration/Children/AgreedDaysAndPickups
        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult _AddMedicalInformation(CreateMedicalInformation model)
        {
            if (ModelState.IsValid)
            {
                return PartialView("_AddMedicalInformation");
            }
            return PartialView(model);
        }
        [HttpPost]
        public PartialViewResult CreateMedicalInformation(string HasDoctor, string HasMedication, string HasAllergies, string HadChildDiseases, CreateMedicalInformation model)
        {
            Toddler toddler = GetCurrentToddler();
            toddler.Medical = new Medical();

            if (HasDoctor == "1")
            {
                toddler.Medical.Doctor = new Doctor();
                toddler.Medical.Doctor.Person = new Person();
                toddler.Medical.Doctor.Person.FirstName = model.medical.Doctor.Person.FirstName;
                toddler.Medical.Doctor.Person.LastName = model.medical.Doctor.Person.LastName;
                toddler.Medical.Doctor.Person.BirthDate = DateTime.Now;
                toddler.Medical.Doctor.Person.RegistrationDate = DateTime.Now;
                toddler.Medical.Doctor.Person.Active = false;

                toddler.Medical.Doctor.Person.Address = new Address();
                toddler.Medical.Doctor.Person.Address.City = model.medical.Doctor.Person.Address.City;
                toddler.Medical.Doctor.Person.Address.PostalCode = model.medical.Doctor.Person.Address.PostalCode;
                toddler.Medical.Doctor.Person.Address.Street = model.medical.Doctor.Person.Address.Street;
                toddler.Medical.Doctor.Person.Address.Number = model.medical.Doctor.Person.Address.Number;
                toddler.Medical.Doctor.Person.Address.Bus = model.medical.Doctor.Person.Address.Bus;

                toddler.Medical.Doctor.Person.ContactDetail = new ContactDetail();
                toddler.Medical.Doctor.Person.ContactDetail.CellPhone = model.medical.Doctor.Person.ContactDetail.CellPhone;
            }
            if (HasMedication == "1")
            {
                toddler.Medical.Medication = true;
                toddler.Medical.MedicationName = model.medical.MedicationName;
            }
            if (HasAllergies == "1")
            {
                toddler.Medical.Allergies = model.medical.Allergies;
            }
            if (HadChildDiseases == "1")
            {
                toddler.Medical.ChildDisease = model.medical.ChildDisease;
            }

            toddler.Medical.HealthServiceNumber = model.medical.HealthServiceNumber;
            toddler.Medical.SpecialNotice = model.medical.SpecialNotice;

            db.Entry(toddler).State = EntityState.Modified;
            db.SaveChanges();

            return PartialView("_ListMedicalInfo", toddler.Medical);
        }


        /**FOODINFORMATION***************/
        // GET: Administration/Children/FoodInformation
        public PartialViewResult _AddFoodAndSleep()
        {

            Toddler toddler = GetCurrentToddler();

            CreateFoodAndSleepInformation cfsi = new CreateFoodAndSleepInformation();
            if (toddler != null)
            {
                cfsi.foodInfo = toddler.Food;
                cfsi.sleepInfo = toddler.Sleep;
            }
            cfsi.food = new Food();
            cfsi.sleep = new Sleep();

            return PartialView(cfsi);

        }
        //POST: Administration/Children/FoodInformation
        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult _AddFoodAndSleep(CreateFoodAndSleepInformation model)
        {
            if (ModelState.IsValid)
            {
                return PartialView("_AddFoodAndSleep");
            }
            return PartialView(model);
        }
        [HttpPost]
        public PartialViewResult CreateFoodInformation(CreateFoodAndSleepInformation model)
        {
            Toddler toddler = GetCurrentToddler();
            toddler.Food = new Food();

            toddler.Food.SpecialDiet = model.food.SpecialDiet;
            toddler.Food.Allergies = model.food.Allergies;
            toddler.Food.MayNotEat = model.food.MayNotEat;
            toddler.Food.BottlePowder = model.food.BottlePowder;
            toddler.Food.BottleDay = model.food.BottleDay;
            toddler.Food.SpecialNotice = model.food.SpecialNotice;

            db.Entry(toddler).State = EntityState.Modified;
            db.SaveChanges();

            return PartialView("_ListFoodInfo", toddler.Food);
        }
        [HttpPost]
        public PartialViewResult CreateSleepInformation(CreateFoodAndSleepInformation model)
        {
            Toddler toddler = GetCurrentToddler();
            toddler.Sleep = new Sleep();

            toddler.Sleep.SleepingPosition = model.sleep.SleepingPosition;
            toddler.Sleep.Toy = model.sleep.Toy;
            toddler.Sleep.Soother = model.sleep.Soother;
            toddler.Sleep.SpecialNotice = model.sleep.SpecialNotice;

            db.Entry(toddler).State = EntityState.Modified;
            db.SaveChanges();

            return PartialView("_ListSleepInfo", toddler.Sleep);
        }

        /**DAILYROUTINE / IMPORTANT NOTICE***************/
        // GET: Administration/Children/DailyRoutine
        public PartialViewResult _AddDailyRoutine()
        {

            Toddler toddler = GetCurrentToddler();

            CreateDailyRoutineAndImportantNotice cdrin = new CreateDailyRoutineAndImportantNotice();
            if (toddler != null)
            {
                cdrin.toddler = toddler;
            } else
            {
                cdrin.toddler = new Toddler();
            }

            return PartialView(cdrin);

        }
        //POST: Administration/Children/DailyRoutine
        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public PartialViewResult _AddDailyRoutine(CreateDailyRoutineAndImportantNotice model)
        {
            if (ModelState.IsValid)
            {
                return PartialView("_AddDailyRoutine");
            }
            return PartialView(model);
        }
        [HttpPost]
        public PartialViewResult CreateDailyRoutine(CreateDailyRoutineAndImportantNotice model)
        {
            Toddler toddler = GetCurrentToddler();
            if (ModelState.IsValid)
            {

                toddler.DailyRoutine = model.toddler.DailyRoutine;

                db.Entry(toddler).State = EntityState.Modified;
                db.SaveChanges();

            }

            return PartialView("_ListDailyRoutine", toddler);
        }
        [HttpPost]
        public PartialViewResult CreateImportantNotice(CreateDailyRoutineAndImportantNotice model)
        {
            Toddler toddler = GetCurrentToddler();
            if (ModelState.IsValid)
            {

                toddler.ImportantNotice = model.toddler.ImportantNotice;

                db.Entry(toddler).State = EntityState.Modified;
                db.SaveChanges();
            }
            return PartialView("_ListImportantNotice", toddler);
        }


        /************** EDIT Pages ************/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Overview");
            }
            else
            {
                Toddler toddler = db.Toddlers
                                    .Where(i => i.ToddlerId == id)
                                    .Include(p => p.Person)
                                    .Include(f => f.Food)
                                    .Include(s => s.Sleep)
                                    .Include(m => m.Medical)
                                    .FirstOrDefault();

                EditToddler ec = new EditToddler();
                ec.toddler = toddler;
                ec.parents = GetParentsOfToddler(toddler);
                ec.pickups = GetPickupsOfToddler(toddler);
                ec.agreedDays = GetAgreedDaysOfToddler(toddler);

                return View(ec);
            }
        }

        [HttpPost]
        public PartialViewResult EditToddler(EditToddler et)
        {

            return PartialView("_OverviewShowToddler", et);
        }


        /** FUNCTIONS *******************************************/

        public string GetNewChildWizardSession()
        {
            string session = "";
            if (!string.IsNullOrEmpty(Session["NewChildWizardSession"].ToString())) {
                session = Session["NewChildWizardSession"].ToString();
            }
            return session;
        }

        public ICollection<RelationLink> GetRelationLinksOfCurrentToddler()
        {
            string session = GetNewChildWizardSession();

            ICollection<RelationLink> RelationLinks = new List<RelationLink>();

            int ToddlerId = int.Parse(db.Toddlers.Where(ts => ts.ToddlerSession.Equals(session)).Select(i => i.ToddlerId).FirstOrDefault().ToString());
            RelationLinks = db.RelationLinks
                .Include(i => i.Person)
                .Include(i => i.Person.Address)
                .Include(i => i.Person.ContactDetail)
                .Where(i => (i.ToddlerId.Equals(ToddlerId)))
                .ToList();

            return RelationLinks;
        }

        public ICollection<RelationLink> GetRelationLinksOfToddler(Toddler toddler)
        {
            ICollection<RelationLink> RelationLinks = new List<RelationLink>();

            RelationLinks = db.RelationLinks
                .Include(i => i.Person)
                .Include(i => i.Person.Address)
                .Include(i => i.Person.ContactDetail)
                .Where(i => (i.ToddlerId.Equals(toddler.ToddlerId)))
                .ToList();

            return RelationLinks;
        }

        public ICollection<Parent> GetParentsOfCurrentToddler()
        {

            ICollection<Parent> Parents = new List<Parent>();
            
            foreach (RelationLink rl in GetRelationLinksOfCurrentToddler())
            {
                foreach (Parent p in db.Parents.Include(i => i.Person).Include(i => i.Person.Address).Include(i => i.Person.ContactDetail).ToList())
                {
                    if (rl.PersonId.Equals(p.PersonId) && rl.RelationToChild.Equals("isParent"))
                    {
                        Parents.Add(p);
                    }
                }
            }

            return Parents;
        }

        public ICollection<Parent> GetParentsOfToddler(Toddler toddler)
        {
            ICollection<Parent> Parents = new List<Parent>();

            foreach (RelationLink rl in GetRelationLinksOfToddler(toddler))
            {
                foreach (Parent p in db.Parents.Include(i => i.Person).Include(i => i.Person.Address).Include(i => i.Person.ContactDetail).ToList())
                {
                    if (rl.PersonId.Equals(p.PersonId) && rl.RelationToChild.Equals("isParent"))
                    {
                        Parents.Add(p);
                    }
                }
            }

            return Parents;
        }

        public ICollection<Toddler> GetToddlersOfCurrentSession()
        {
            string session = GetNewChildWizardSession();

            ICollection<Toddler> Toddlers = db.Toddlers.Where(s => s.ToddlerSession.Equals(session)).Include(i => i.Person).ToList();

            return Toddlers;
        }

        public Toddler GetCurrentToddler()
        {
            string session = GetNewChildWizardSession();
            Toddler toddler = db.Toddlers.Where(s => s.ToddlerSession.Equals(session))

                .Include(i => i.Person)
                .Include(i => i.Medical)
                .Include(i => i.Sleep)
                .Include(i => i.Food)
                
                .FirstOrDefault();
            return toddler;
        }

        public ICollection<AgreedDays> GetAgreedDaysOfCurrentToddler()
        {
            Toddler toddler = GetCurrentToddler();
            
            ICollection<AgreedDays> agreedDaysList = new List<AgreedDays>(); 
            if (toddler != null) { agreedDaysList = db.AgreedDays.Where(t => t.ToddlerId == toddler.ToddlerId).ToList(); }
            return agreedDaysList;
        }

        public ICollection<AgreedDays> GetAgreedDaysOfToddler(Toddler toddler)
        {
            ICollection<AgreedDays> agreedDaysList = new List<AgreedDays>();
            if (toddler != null) { agreedDaysList = db.AgreedDays.Where(t => t.ToddlerId == toddler.ToddlerId).ToList(); }
            return agreedDaysList;
        }

        public ICollection<Pickup> GetPickupsOfCurrentToddler()
        {
            Toddler toddler = GetCurrentToddler();
            ICollection<RelationLink> relationLinks = GetRelationLinksOfCurrentToddler();
            ICollection<Pickup> pickups = new List<Pickup>();

            foreach (RelationLink rl in GetRelationLinksOfCurrentToddler())
            {
                foreach (Pickup p in db.Pickups.Include(i => i.Person).Include(i => i.Person.Address).Include(i => i.Person.ContactDetail).ToList())
                {
                    if (rl.PersonId.Equals(p.PersonId) && rl.RelationToChild.Equals("isPickup"))
                    {
                        pickups.Add(p);
                    }
                }
            }

            return pickups;

        }

        public ICollection<Pickup> GetPickupsOfToddler(Toddler toddler)
        {
            ICollection<Pickup> Pickups = new List<Pickup>();

            foreach (RelationLink rl in GetRelationLinksOfToddler(toddler))
            {
                foreach (Pickup p in db.Pickups.Include(i => i.Person).Include(i => i.Person.Address).Include(i => i.Person.ContactDetail).ToList())
                {
                    if (rl.PersonId.Equals(p.PersonId) && rl.RelationToChild.Equals("isPickup"))
                    {
                        Pickups.Add(p);
                    }
                }
            }

            return Pickups;
        }

        public string CalculateReadyForDaycare(DateTime birthdate)
        {

            DateTime readyForDaycare;
            // SETTINGOPTION!!!
            readyForDaycare = birthdate.AddMonths(2);
            string date = readyForDaycare.ToString("dd'/'MM'/'yyyy");
            return date;
        }

        public string CalculateReadyForSchool(DateTime birthdate)
        {
            DateTime readyForSchool;
            SchoolHolidays schoolHolidays = new SchoolHolidays(birthdate);

            readyForSchool = schoolHolidays.CanGoToSchoolFrom();
            string date = readyForSchool.ToString("dd'/'MM'/'yyyy");
            return date;
        }

        public void SendFirstMail(string email, string password)
        {
            MailMessage mail = new MailMessage("noreply@detutjes.be", email);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "relay.proximus.be";
            mail.Subject = "Je account bij De Tutjes";
            mail.Body = "Welkom bij de tutjes, je wachtwoord is " + password;
            client.Send(mail);
        }

        // JQUERY AJAX FUNCTIONS

        [HttpPost]
        public JsonResult CalculateFreePlacesOnDayAJAX(string day, string startdate, string enddate)
        {
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            // CONVERT JQUERY TO DATETIME
            string format = "ddd MMM dd yyyy HH:mm:ss";
            if (string.IsNullOrEmpty(day) && string.IsNullOrEmpty(startdate) && string.IsNullOrEmpty(enddate))
            {
                startdate = startdate.Substring(0, 24);
                enddate = enddate.Substring(0, 24);
                bool  isValidStartDate = DateTime.TryParseExact(startdate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate);
                bool isValidEndDate = DateTime.TryParseExact(enddate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate);
            }

            int max = 17;
            //int freeplaces = 0;
            ICollection<DateTime> newPeriod = new List<DateTime>();

            ICollection<AgreedDays> agreedDaysList = db.AgreedDays.Where(st => st.EndDate >= StartDate).ToList();

            double totalDays = (EndDate - StartDate).TotalDays;
            double i;
            for (i = 0; i < totalDays; i++)
            {
                DateTime date = StartDate.AddDays(i);
                switch (date.ToString("dddd"))
                {
                    case "maandag":
                    case "dinsdag":
                    case "woensdag":
                    case "donderdag":
                    case "vrijdag":
                        newPeriod.Add(date);
                        break;
                    case "zaterdag":
                    case "zondag":
                    default:
                        break;
                }
            }
                foreach (AgreedDays agreedDay in agreedDaysList)
                {
                    ICollection<DateTime> existingPeriod = new List<DateTime>();
                    double existingTotalDays = (agreedDay.EndDate - agreedDay.StartDate).TotalDays;
                    double i2;
                    for (i2 = 0; i2 < totalDays; i2++)
                    {
                        DateTime date2 = StartDate.AddDays(i);
                        switch (date2.ToString("dddd"))
                        {
                            case "maandag":
                            case "dinsdag":
                            case "woensdag":
                            case "donderdag":
                            case "vrijdag":
                                existingPeriod.Add(date2);
                                break;
                            case "zaterdag":
                            case "zondag":
                            default:
                                break;
                        }
                    }
                    foreach (DateTime existingDay in existingPeriod)
                    {
                        if (newPeriod.Contains(existingDay))
                        {
                            max--;
                        }
                    }
                }


            return Json(new { freeplace = max });
        }
        [HttpPost]
        public JsonResult CalculateReadyDateAJAX(string birthdate)
        {
            string format = "ddd MMM dd yyyy HH:mm:ss";
            if (string.IsNullOrEmpty(birthdate))
            {
                birthdate = birthdate.Substring(0, 24);

                DateTime birthdateDT;
                DateTime readyForSchool;
                DateTime readyForDaycare;

                bool validFormat = DateTime.TryParseExact(birthdate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdateDT);
                Console.Write(validFormat ? birthdateDT.ToString() : "Not a valid format");

                SchoolHolidays schoolHolidays = new SchoolHolidays(birthdateDT);

                readyForSchool = schoolHolidays.CanGoToSchoolFrom();

                // SETTINGOPTION!!!
                readyForDaycare = birthdateDT.AddMonths(2);

                var result = new { readyForSchool = readyForSchool.ToString("dd'/'MM'/'yyyy"), readyForDaycare = readyForDaycare.ToString("dd'/'MM'/'yyyy") };

                CreateAgreedDays_StartDate = readyForDaycare;
                CreateAgreedDays_EndDate = readyForSchool;

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string dateDaycare = CalculateReadyForDaycare(GetCurrentToddler().Person.BirthDate);
                string dateSchool = CalculateReadyForSchool(GetCurrentToddler().Person.BirthDate);
                var result = new { readyForSchool = dateSchool, readyForDaycare = dateDaycare };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

    }
}