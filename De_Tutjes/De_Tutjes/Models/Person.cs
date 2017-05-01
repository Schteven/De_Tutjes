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
        [Key]
        public int PersonId { get; set; }
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }
        [Display(Name = "Geslacht")]
        public string Gender { get; set; }

        [Display(Name = "Geboortedatum")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
        [ForeignKey("ContactDetail")]
        public int? ContactDetailId { get; set; }
        public virtual ContactDetail ContactDetail { get; set; }
        [ForeignKey("UserAccount")]
        public string UserAccountId { get; set; }
        public virtual ApplicationUser UserAccount { get; set; }

        public virtual ICollection<RelationLink> RelationLinks { get; set; }


        public Person()
        {

        }

    }
}
