using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    public class ContactDetail
    {
        [Key]
        public int ContactDetailsId { get; set; }
        public int HomePhone { get; set; }
        public int CellPhone { get; set; }
        public int WorkPhone { get; set; }
        public string Email { get; set; }
    }
}