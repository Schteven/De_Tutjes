using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("Sleeping")]
    public class Sleep
    {
        public int SleepID { get; set; }
        [Display(Name = "Slaappositie")]
        public string SleepingPosition { get; set; }
        [Display(Name = "Heeft het kind een knuffel?")]
        public bool HasToy { get; set; }
        [Display(Name = "Knuffel")]
        public string Toy { get; set; }
        [Display(Name = "Heeft het kind een tutje?")]
        public bool HasSoother { get; set; }
        [Display(Name = "Tutje")]
        public string Soother { get; set; }
        [Display(Name = "Speciale opmerking")]
        public string SpecialNotice { get; set; }

        public virtual ICollection<Toddler> Toddlers { get; set; }

        public Sleep()
        {

        }
    }
}
