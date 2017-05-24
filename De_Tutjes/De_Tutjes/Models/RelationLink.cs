using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("RelationLinks")]
    public class RelationLink
    {
        [Key]
        public int RelationLinkID { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string RelationToChild { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        [ForeignKey("Toddler")]
        public int ToddlerId { get; set; }
        public virtual Toddler Toddler { get; set; }

        public RelationLink()
        {

        }
    }
}
