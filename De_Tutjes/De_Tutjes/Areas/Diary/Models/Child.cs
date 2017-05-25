using De_Tutjes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace De_Tutjes.Areas.Diary.Models
{
    public enum ChildStatus
    {
        Home = 1,
        Sleeping = 2,
        Normal = 3
    }
    public class Child
    {
        public ChildStatus Status { get; set; }
        public Toddler Toddler { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public DiaryToddlerStatus dts { get; set; }
        public ICollection<Parent> Parents { get; set; }
        //public Parent ParentOne { get; set; }
        //public Parent ParentTwo { get; set; }
        public Child()
        {

        }
        public Child(Toddler toddler, ICollection<Parent> parents, ChildStatus status)
        {
            this.Status = status;
            this.Toddler = toddler;
            this.Id = toddler.ToddlerId.ToString();
            this.Name = toddler.Person.FirstName;
            this.Photo = toddler.Person.Photo;
            this.Parents = parents;
        }







    }
}