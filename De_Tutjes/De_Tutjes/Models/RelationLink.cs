using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    class RelationLink
    {
        public int RelationLinkID { get; set; }
        public int ToddlerID { get; set; }
        public int FirstParentID { get; set; }
        public int SecondParentID { get; set; }
        public int FirstPickupID { get; set; }
        public int SecondPickupID { get; set; }

        public RelationLink()
        {

        }
    }
}
