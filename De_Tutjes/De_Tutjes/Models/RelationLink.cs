using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("RelationLinks")]
    public class RelationLink
    {
        public int RelationLinkID { get; set; }
        public string RelationToChild { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
        public virtual Toddler Toddler { get; set; }

        public RelationLink()
        {

        }
    }
}
