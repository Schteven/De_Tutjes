using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using De_Tutjes.Models;

namespace De_Tutjes.Areas.Administration.Models
{
    public class EditToddler
    {
        public Toddler toddler { get; set; }
        public ICollection<Parent> parents { get; set; }
        public ICollection<Pickup> pickups { get; set; }
        public ICollection<AgreedDays> agreedDays { get; set; }
    }
}
