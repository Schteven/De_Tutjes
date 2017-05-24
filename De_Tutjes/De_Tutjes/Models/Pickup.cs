using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("Pickups")]
    public class Pickup
    {
        [Key]
        public int PickupId { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Relatie")]
        public string Relation { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        public Pickup()
        {

        }
    }
}
