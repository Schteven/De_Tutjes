using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace De_Tutjes.Areas.Diary.Models
{
    public class ChildCard
    {
        public ChildStatus Status { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }

        public ChildCard()
        {

        }
        public ChildCard(string id, string name, string photo, ChildStatus status)
        {
            this.Status = status;
            this.Id = id;
            this.Name = name;
            this.Photo = photo;
        }
    }
}