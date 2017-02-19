using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("Pickups")]
    public class Pickup : Person
    {
        public int PickupId { get; set; }
        public string Relation { get; set; }

        public Pickup()
        {

        }
    }
}
