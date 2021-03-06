﻿using De_Tutjes.Areas.Diary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using De_Tutjes.Models;
using System.Data.Entity;

namespace De_Tutjes.Areas.Diary.Controllers
{
    public enum ChildUpdate
    {
        CheckIn = 1,
        Sleeping = 2,
        WakeUp = 3,
        Eating = 4,
        Diaper = 5,
        CheckOut = 6,
        Comment = 7
    }
    public class ChildManager
    {
        private List<Child> children = new List<Child>();
        private List<ChildCard> childcards = new List<ChildCard>();
        private DeTutjesContext db = new DeTutjesContext();
        private Location location;
        
        public ChildManager()
        {
            fillChildrenList();
        }
        public ChildManager(Location location)
        {
            fillChildrenList();
            this.location = location;
        }
        public void SetLocation(Location loc)
        {
            this.location = loc;
        }

        // PUBLIC  FUNCTIONS
        // return all Childs from children
        public List<Child> GetAllChilds()
        {
            return children;
        }
        // return Child for Partial View child informatie
        public Child GetChildById(string id)
        {
            Child child = new Child();
            child.Toddler = getToddler(Int32.Parse(id));
            child.dts = getDiaryToddlerStatus(child.Toddler);
            child.Status = (ChildStatus)child.dts.Status;
            child.Parents = GetParentsOfToddler(child.Toddler.ToddlerId);
            return child;
        }
        // return ChildCard for each child that has to come today
        public List<ChildCard> GetChildCardsForToday()
        {
            DateTime today = DateTime.Now;
            DateTime todaydate = DateTime.Now.Date;
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            List<AgreedDays> agreedDaysList = whoHasToComeToday(dayOfWeek, today);
            List<ChildCard> ccList = new List<ChildCard>();
            if (agreedDaysList != null)
            {
                foreach (AgreedDays AD in agreedDaysList)
                {
                    Toddler tod = getToddler(AD.ToddlerId);
                    if (tod != null)
                    {
                        ChildCard cc = new ChildCard();
                        cc.Id = tod.ToddlerId.ToString();
                        cc.Name = tod.Person.FirstName;
                        cc.Photo = tod.Person.Photo;
                        cc.Status = (ChildStatus)getDiaryToddlerStatus(tod).Status;
                        ccList.Add(cc);


                        // Creating RegisteredDay with checkedIn == false
                        
                        RegisteredDay rd = db.RegisteredDays.Where(rdc => (rdc.ToddlerId == tod.ToddlerId) && (rdc.DayInDaycare == todaydate)).FirstOrDefault();
                        if(rd == null || rd.RegisteredDayId == 0)
                        {
                            rd = new RegisteredDay();
                            rd.DayInDaycare = todaydate;
                            rd.CheckedIn = false;
                            rd.DaycareIsClosed = false;
                            rd.ExtraDay = false;
                            rd.Toddler = tod;
                            db.RegisteredDays.Add(rd);
                            db.SaveChanges();
                        }
                    }
                }
            }
            List<RegisteredDay> extraDays = db.RegisteredDays.Include(erd => erd.Toddler).Include(erd => erd.Toddler.Person).Where(erd => (erd.ExtraDay == true) && (erd.DayInDaycare == todaydate)).ToList();
            if (extraDays != null)
            {
                foreach (RegisteredDay rd in extraDays)
                {
                    ChildCard cc = new ChildCard();
                    cc.Id = rd.ToddlerId.ToString();
                    cc.Name = rd.Toddler.Person.FirstName;
                    cc.Photo = rd.Toddler.Person.Photo;
                    cc.Status = (ChildStatus)getDiaryToddlerStatus(rd.Toddler).Status;
                    ccList.Add(cc);
                }
            }
            return ccList;
        }
        
        public List<Child> GetChildrenWithUpdates()
        {
            List<Child> childrenWithUpdates = new List<Child>();
            /*
             weekend test
             
                Child c = GetChildById("57");
                children.Add(c);
                Child c1 = GetChildById("58");
                children.Add(c1);
            
            */

            foreach (Child cu in children)
            {
                List<DiaryToddlerUpdate> dtu = db.DiaryToddlerUpdate.Where(d => (d.ToddlerId.Equals(cu.Toddler.ToddlerId) && (d.Timestamp >= DateTime.Today))).ToList();
                Child cwu = cu;
                cwu.Updates = dtu;
                childrenWithUpdates.Add(cwu);
            }


            return childrenWithUpdates;
        }

        public Child GetChildWithUpdates(string toddlerID)
        {
            if (toddlerID != null)
            {
                int id = int.Parse(toddlerID);
                List<DiaryToddlerUpdate> dtu = db.DiaryToddlerUpdate.Where(d => (d.ToddlerId.Equals(id) && (d.Timestamp >= DateTime.Today))).ToList();
                Child cwu = GetChildById(toddlerID);
                cwu.Updates = dtu;
                return cwu;
            }
            return null;
            
        }

        public List<ChildCard> GetChildCards()
        {
            DateTime today = DateTime.Now;
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            List<Toddler> toddlers = db.Toddlers.Include(t => t.Person).Where(t => t.Person.Active == true).ToList();
            List<ChildCard> ccList = new List<ChildCard>();
            if (toddlers != null)
            {
                foreach (Toddler t in toddlers)
                {
                    Toddler tod = t;
                    if (tod != null)
                    {
                        ChildCard cc = new ChildCard();
                        cc.Id = tod.ToddlerId.ToString();
                        cc.Name = tod.Person.FirstName;
                        cc.Photo = tod.Person.Photo;
                        cc.Status = (ChildStatus)getDiaryToddlerStatus(tod).Status;
                        ccList.Add(cc);
                        
                    }
                }
            }
            return ccList;
        }

        // SET FUNCTIONS
        // Sets the Status (Home, Sleeping, Normal) of the child
        public void SetChildStatus(string ChildId, ChildStatus status)
        {
            foreach (Child c in children)
            {
                if (c.Id == ChildId)
                {
                    ChildStatus realStatus = status;
                    if(status == ChildStatus.Sleeping && c.Status == ChildStatus.Sleeping)
                    {
                        realStatus = ChildStatus.Normal;
                    }
                    if(status == ChildStatus.Sleeping && c.Status == ChildStatus.Home)
                    {
                        return;
                    }
                    //Send to database DiaryToddlerStatus
                    try
                    {
                        DiaryToddlerStatus dts = getDiaryToddlerStatus(c.Toddler);
                        dts.Status = (int)realStatus;
                        db.SaveChanges();
                        
                        children.First(ch => ch.Id == ChildId).Status = realStatus;
                    }
                    catch
                    {

                    }
                    
                }
            }
        }
        // Sends the child update to the database (diary entry)
        public void SetChildUpdate(string ChildId, ChildUpdate update, String comment)
        {
            foreach (Child c in children)
            {
                if (c.Id == ChildId)
                {
                    //Toggles between sleep and wake up (because its only one button)
                    ChildUpdate realUpdate = update;
                    if (c.Status == ChildStatus.Sleeping && update == ChildUpdate.Sleeping)
                    {
                        realUpdate = ChildUpdate.WakeUp;
                    }
                    //If the child is at home, you can't do anything else than check it in or place a comment. (safetycheck)
                    if(c.Status == ChildStatus.Home && (update != ChildUpdate.CheckIn && update != ChildUpdate.Comment)){
                        return;
                    }
                    // CheckIn on registeredday
                    if(update == ChildUpdate.CheckIn)
                    {
                        DateTime todaydate = DateTime.Now.Date;
                        
                        RegisteredDay rd = db.RegisteredDays.Where(rdc => (rdc.ToddlerId == c.Toddler.ToddlerId) && (rdc.DayInDaycare == todaydate)).FirstOrDefault();
                        if (rd != null || rd.ToddlerId != 0)
                        {
                            rd.CheckedIn = true;
                            db.SaveChanges();
                        }
                        
                    }
                    //Send diary entry to database (DiaryToddlerUpdate)
                    try
                    {                        
                        DiaryToddlerUpdate dtu = new DiaryToddlerUpdate();
                        dtu.Toddler = db.Toddlers.Find(c.Toddler.ToddlerId);
                        dtu.Timestamp = DateTime.Now;
                        dtu.UpdateType = (int)realUpdate;
                        if (comment != null) dtu.Comment = comment;
                        db.DiaryToddlerUpdate.Add(dtu);
                        db.SaveChanges();
                    }
                    catch { }
                }
            }
        }
        // Add ExtraDay for a child
        public void AddExtraDay(string childId)
        {
            int idint = 0;
            bool tryit = int.TryParse(childId, out idint);
            if (idint!= 0)
            {
                Toddler tod = getToddler(idint);
                if(tod != null)
                {
                    RegisteredDay rd = new RegisteredDay();
                    rd.DayInDaycare = DateTime.Now.Date;
                    rd.CheckedIn = false;
                    rd.DaycareIsClosed = false;
                    rd.ExtraDay = true;
                    rd.Toddler = tod;
                    db.RegisteredDays.Add(rd);
                    db.SaveChanges();
                }
            }
            
            
        }

        // PRIVAT HELPER FUNCTIONS
        private void fillChildrenList()
        {
            DateTime today = DateTime.Now;
            DateTime todaydate = DateTime.Now.Date;
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            List<AgreedDays> agreedDaysList = whoHasToComeToday(dayOfWeek, today);
            
            if (agreedDaysList != null)
            {
                foreach (AgreedDays AD in agreedDaysList)
                {
                   Toddler tod = getToddler(AD.ToddlerId);
                    if (tod != null)
                    {
                        Child child = new Child();
                        child.Id = tod.ToddlerId.ToString();
                        child.Toddler = tod;
                        child.dts = getDiaryToddlerStatus(tod);
                        child.Status = (ChildStatus)child.dts.Status;
                        child.Parents = GetParentsOfToddler(tod.ToddlerId);
                        children.Add(child);

                        
                    }
                }
            }
            List<RegisteredDay> extraDays = db.RegisteredDays.Include(erd => erd.Toddler).Include(erd => erd.Toddler.Person).Where(erd => (erd.ExtraDay == true) && (erd.DayInDaycare == todaydate)).ToList();
            if (extraDays != null)
            {
                foreach (RegisteredDay rd in extraDays)
                {
                    Toddler tod = getToddler(rd.ToddlerId);
                    if (tod != null)
                    {
                        Child child = new Child();
                        child.Id = tod.ToddlerId.ToString();
                        child.Toddler = tod;
                        child.dts = getDiaryToddlerStatus(tod);
                        child.Status = (ChildStatus)child.dts.Status;
                        child.Parents = GetParentsOfToddler(tod.ToddlerId);
                        children.Add(child);
                    }
                }
            }
        }

        private Toddler getToddler(int id)
        {
            Toddler tod = new Toddler();
            try
            {
                tod = db.Toddlers.Include(p => p.Person)
                                    .Include(p => p.Food)
                                    .Include(p => p.Medical)
                                    .Include(p => p.Sleep)
                                    .Where(p => p.ToddlerId == id)
                                    .First();
            }
            catch (Exception)
            {

                throw;
            }
            return tod;
        }

        private ICollection<Parent> GetParentsOfToddler(int id)
        {
            ICollection<Parent> Parents = new List<Parent>();
            Toddler t = getToddler(id);
            foreach (RelationLink rl in GetRelationLinksOfToddler(t))
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

        private ICollection<RelationLink> GetRelationLinksOfToddler(Toddler toddler)
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
        //returns AgreedDays from the day of the week and when today is between start and end day
        private List<AgreedDays> whoHasToComeToday(string dayofWeek, DateTime today)
        {
            string dOW = dayofWeek;
            List<AgreedDays> agreedDaysList = new List<AgreedDays>();
            switch (dOW)
            {
                case "Monday":
                    agreedDaysList = db.AgreedDays.Where(agreed =>
                                                            agreed.StartDate <= today &&
                                                            agreed.EndDate >= today &&
                                                            agreed.Monday == true
                                                            ).ToList();
                    break;
                case "Tuesday":
                    agreedDaysList = db.AgreedDays.Where(agreed =>
                                                            agreed.StartDate <= today &&
                                                            agreed.EndDate >= today &&
                                                            agreed.Tuesday == true
                                                            ).ToList();
                    break;
                case "Wednesday":
                    agreedDaysList = db.AgreedDays.Where(agreed =>
                                                            agreed.StartDate <= today &&
                                                            agreed.EndDate >= today &&
                                                            agreed.Wednesday == true
                                                            ).ToList();
                    break;
                case "Thursday":
                    agreedDaysList = db.AgreedDays.Where(agreed =>
                                                            agreed.StartDate <= today &&
                                                            agreed.EndDate >= today &&
                                                            agreed.Thursday == true
                                                            ).ToList();
                    break;
                case "Friday":
                    agreedDaysList = db.AgreedDays.Where(agreed =>
                                                            agreed.StartDate <= today &&
                                                            agreed.EndDate >= today &&
                                                            agreed.Friday == true
                                                            ).ToList();
                    break;
                default:
                    break;
            }
            return agreedDaysList;
        }
        //returns the DiaryToddlerStatus line for a given toddler (and location)
        private DiaryToddlerStatus getDiaryToddlerStatus(Toddler tod)
        {
            DiaryToddlerStatus dts = db.DiaryToddlerStatus.FirstOrDefault(d => d.ToddlerId == tod.ToddlerId);//add location search

            if (dts == null)
            {
                DiaryToddlerStatus createNewDts = new DiaryToddlerStatus();
                createNewDts.Toddler = tod;
                createNewDts.Status = (int)ChildStatus.Home;
                //add location
                if(location!=null)createNewDts.Location = location;
                try
                {
                    db.DiaryToddlerStatus.Add(createNewDts);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                dts = db.DiaryToddlerStatus.First(d => d.ToddlerId == tod.ToddlerId);
            }
            return dts;
        }

        
    }
}