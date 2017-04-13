using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("Toddlers")]
    public class Toddler
    {
        [Key]
        public int ToddlerId { get; set; }

        public string ToddlerSession { get; set; }
        public string DailyRoutine { get; set; }
        public string ImportantNotice { get; set; }

        [ForeignKey("Food")]
        public int? FoodId { get; set; }
        public virtual Food Food { get; set; }
        [ForeignKey("Medical")]
        public int? MedicalId { get; set; }
        public virtual Medical Medical { get; set; }
        [ForeignKey("Sleep")]
        public int? SleepId { get; set; }
        public virtual Sleep Sleep { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        public virtual ICollection<RelationLink> RelationLinks { get; set; }

        public virtual ICollection<AgreedDays> AgreedDays { get; set; }

        public Toddler()
        {

        }
    }
}
