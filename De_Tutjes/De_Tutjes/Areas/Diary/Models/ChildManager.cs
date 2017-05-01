using De_Tutjes.Areas.Diary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


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
        
        //temp filler
        private void addChild(Child child)
        {
            children.Add(child);
        }

        public ChildManager()
        {
            //TODO: load Children from database

            //temp
            addChild(new Child("Child001", "Milan" ,    ChildStatus.Home ,  "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Baby_Face.JPG/220px-Baby_Face.JPG"));
            addChild(new Child("Child002", "Stefanie",  ChildStatus.Normal, "https://dfcs.georgia.gov/sites/dfcs.georgia.gov/files/styles/rotator_box/public/rotator/report-child-abuse-slide.jpg?itok=9kgCp7lN"));
            addChild(new Child("Child003", "Ella-Noor", ChildStatus.Normal, "http://kingofwallpapers.com/child/child-012.jpg"));
            addChild(new Child("Child004", "Emma",      ChildStatus.Home,   ""));
            addChild(new Child("Child005", "Mika",      ChildStatus.Normal, "https://skincancer.blob.core.windows.net/assets/uploads/img/Guide.to.Healthy.Skin/infant.1.jpg"));
            addChild(new Child("Child006", "Linn",      ChildStatus.Normal, ""));
            addChild(new Child("Child007", "Arthuur",   ChildStatus.Home,   ""));
            addChild(new Child("Child008", "Louis",     ChildStatus.Normal, ""));
            addChild(new Child("Child009", "Alexander", ChildStatus.Home,   "http://sonshinelearningcenter.net/wp-content/uploads/2015/12/Surprised-Older-Infant.jpg"));
            addChild(new Child("Child010", "Gazell",    ChildStatus.Sleeping, ""));
            addChild(new Child("Child011", "Kimbra",    ChildStatus.Sleeping, ""));
            addChild(new Child("Child012", "Finn",      ChildStatus.Normal, "https://www.drgreene.com/wp-content/uploads/Infant-Constipation.jpg"));
        }
        

        public List<Child> GetAllChilds()
        {
            //return all childs
            
            
            return children;
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