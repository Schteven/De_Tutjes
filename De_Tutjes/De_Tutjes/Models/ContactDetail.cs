﻿using System;
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
        [DisplayFormat(NullDisplayText = "")]
        public int? HomePhone { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public int CellPhone { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public int? WorkPhone { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string Email { get; set; }

        [ForeignKey("Person")]
        public int? PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}