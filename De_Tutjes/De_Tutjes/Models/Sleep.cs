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
        public string SleepingPosition { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public bool HasToy { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string Toy { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public bool HasSoother { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string Soother { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string SpecialNotice { get; set; }

        public virtual ICollection<Toddler> Toddlers { get; set; }

        public Sleep()
        {

        }
    }
}
