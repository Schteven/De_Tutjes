using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("Persons")]
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }

        public virtual Address Address { get; set; }
        public virtual ContactDetail ContactDetail { get; set; }
        public virtual ApplicationUser UserAccount { get; set; }
        public virtual ICollection<RelationLink> RelationLinks { get; set; }


        public Person()
        {

        }

    }
}
