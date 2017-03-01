using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("Toddlers")]
    public class Toddler
    {
        public int ToddlerId { get; set; }
        public string DailyRoutine { get; set; }
        public string ImportantNotice { get; set; }

        public virtual Food Food { get; set; }
        public virtual Medical Medical { get; set; }
        public virtual Sleep Sleep { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<RelationLink> RelationLinks { get; set; }

        public Toddler()
        {

        }
    }
}
