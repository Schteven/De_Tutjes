using De_Tutjes.Areas.Diary.Models;
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
        private DeTutjesContext db = new DeTutjesContext();
        private Location location;
        
        private void addChild(Child child)
        {
            children.Add(child);
        }

        public ChildManager()
        {
            //TODO: load Toddlers from database
            CreateChildren();
            //temp
        }
        public void SetLocation(Location loc)
        {
            this.location = loc;
        }
        public void CreateChildren()
        {
            List<Toddler> tods = GetAllToddlers();

            foreach (Toddler t in tods)
            {
                Child c = new Child(t, ChildStatus.Home);
                addChild(c);
            }
        }
        public List<Child> GetAllChilds()
        {
            return children;
        }
        public List<Toddler> GetAllToddlers()
        {
            //return all childs
            List<Toddler> toddlers = new List<Toddler>();
            try
            {
                toddlers = db.Toddlers.Where(a => a.Person.Active == true)
                .Include(p => p.Person)
                .Include(f => f.Food)
                .Include(s => s.Sleep)
                .Include(m => m.Medical)
                .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return toddlers;
        }


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
                        DiaryToddlerStatus dts = new DiaryToddlerStatus();
                        dts.Toddler = db.Toddlers.Find(c.Toddler.ToddlerId);
                        dts.Status = (int)realStatus;
                        db.DiaryToddlerStatus.Add(dts);
                        
                        c.Status = status;
                    }
                    catch
                    {

                    }
                    
                }
            }
        }
        public void SetChildUpdate(string ChildId, ChildUpdate update, String comment)
        {
            foreach (Child c in children)
            {
                if (c.Id == ChildId)
                {
                    ChildUpdate realUpdate = update;
                    if (c.Status == ChildStatus.Sleeping && update == ChildUpdate.Sleeping)
                    {
                        realUpdate = ChildUpdate.WakeUp;
                    }
                    if(c.Status == ChildStatus.Home && (update != ChildUpdate.CheckIn && update != ChildUpdate.Comment)){
                        return;
                    }
                    //Send to database DiaryToddlerUpdate
                    try
                    {                        
                        DiaryToddlerUpdate dtu = new DiaryToddlerUpdate();
                        dtu.Toddler = db.Toddlers.Find(c.Toddler.ToddlerId);
                        dtu.Timestamp = DateTime.Now;
                        dtu.UpdateType = (int)update;
                        if (comment != null) dtu.Comment = comment;
                        db.DiaryToddlerUpdate.Add(dtu);
                    }
                    catch { }
                }
            }
        }





    }
}