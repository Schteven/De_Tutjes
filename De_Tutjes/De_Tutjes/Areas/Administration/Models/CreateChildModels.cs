using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using De_Tutjes.Models;
using System.ComponentModel;

namespace De_Tutjes.Areas.Administration.Models
{
    public class StartWizardOverview
    {
        public RelationLink relationLink { get; set; }
        public ICollection<RelationLink> relationLinks { get; set; }
    }

    public class CreateToddlerOverview
    {
        public Toddler toddler { get;  set;}
        public ICollection<Toddler> toddlers { get; set; }
    }

    public class CreateParentsOverview
    {
        public Parent parent { get; set; }
        public ICollection<Parent> parents { get; set; }
        public ICollection<RelationLink> relationLinks { get; set; }
    }

    public class CreateAgreedDaysAndPickup
    {
        public AgreedDays agreedDays { get; set; }
        public Pickup pickup { get; set; }
        public ICollection<AgreedDays> agreedDaysList { get; set; }
        public ICollection<Pickup> pickups { get; set; }
        public ICollection<RelationLink> relationLinks { get; set; }
    }

}