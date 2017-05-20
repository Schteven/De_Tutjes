using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    [Table("ContactDetails")]
    public class ContactDetail
    {
        [Key]
        public int ContactDetailId { get; set; }
        [Display(Name = "Huistelefoon")]
        public int? HomePhone { get; set; }
        [Display(Name = "Mobiel")]
        public int CellPhone { get; set; }
        [Display(Name = "Werk")]
        public int? WorkPhone { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [ForeignKey("Person")]
        public int? PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}