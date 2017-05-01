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
        Eating = 3,
        Diaper = 4,
        CheckOut = 5
    }
    public class ChildManager
    {
        private List<Child> children = new List<Child>();
        private DeTutjesContext db = new DeTutjesContext();
        
        //temp filler
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
            List<Toddler> toddlers = db.Toddlers.Where(a => a.Person.Active == true)
                .Include(p => p.Person)
                .Include(f => f.Food)
                .Include(s => s.Sleep)
                .Include(m => m.Medical)
                .ToList();

            return toddlers;
        }


        public void SetChildStatus(string ChildId, ChildStatus status)
        {
            foreach (Child c in children)
            {
                if (c.Id == ChildId)
                {
                    c.Status = status;
                }
            }
        }
        public void SetChildUpdate(string ChildId, ChildUpdate update)
        {
            foreach (Child c in children)
            {
                if (c.Id == ChildId)
                {
                    //Send to database history
                }
            }
        }





    }
}