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
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Slaappositie")]
        public string SleepingPosition { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Heeft het kind een knuffel?")]
        public bool HasToy { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Knuffel")]
        public string Toy { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Heeft het kind een tutje?")]
        public bool HasSoother { get; set; }
        [Display(Name = "Tutje")]
        [DisplayFormat(NullDisplayText = "")]
        public string Soother { get; set; }
        [Display(Name = "Speciale opmerking")]
        [DisplayFormat(NullDisplayText = "")]
        public string SpecialNotice { get; set; }

        public virtual ICollection<Toddler> Toddlers { get; set; }

        public Sleep()
        {

        }
    }
}
