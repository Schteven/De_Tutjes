using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("Parents")]
    public class Parent
    {
        [Key]
        public int ParentID { get; set; }
        public string Relation { get; set; } // Mother or father

        public virtual Person Person { get; set; }

        public Parent()
        {

        }
    }
}
