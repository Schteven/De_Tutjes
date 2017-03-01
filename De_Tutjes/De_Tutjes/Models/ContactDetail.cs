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
        public int? HomePhone { get; set; }
        public int CellPhone { get; set; }
        public int? WorkPhone { get; set; }
        public string Email { get; set; }

        public virtual Person Person { get; set; }
    }
}