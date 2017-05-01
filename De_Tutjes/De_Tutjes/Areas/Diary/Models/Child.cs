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
        public string Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public Child()
        {

        }
        public Child(string id, string name, ChildStatus status, string link )
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
            this.Picture = link;
        }







    }
}