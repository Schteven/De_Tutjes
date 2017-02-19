using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    class Pickup : Person
    {
        public int PickupId { get; set; }
        public string Relation { get; set; }

        public Pickup()
        {

        }
    }
}
