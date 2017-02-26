using System;
using System.Collections.Generic;
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
        public string SleepingPosition { get; set; }
        public bool HasToy { get; set; }
        public string Toy { get; set; }
        public bool HasSoother { get; set; }
        public string Soother { get; set; }
        public string SpecialNotice { get; set; }

        public virtual ICollection<Toddler> Toddlers { get; set; }

        public Sleep()
        {

        }
    }
}
