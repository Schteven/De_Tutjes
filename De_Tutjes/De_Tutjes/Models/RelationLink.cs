using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    public class RelationLink
    {
        public int RelationLinkID { get; set; }
        public int ToddlerID { get; set; }
        public int PersonID { get; set; }

        public RelationLink()
        {

        }
    }
}
