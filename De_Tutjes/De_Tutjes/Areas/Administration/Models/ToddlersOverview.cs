using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using De_Tutjes.Models;

namespace De_Tutjes.Areas.Administration.Models
{
    public class ToddlersOverview
    {
        public Toddler toddler { get; set; }
        public ICollection<Toddler> toddlerList { get; set; }
    }
}