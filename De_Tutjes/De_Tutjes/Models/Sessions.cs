using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    [Table("NewChildWizardSessions")]
    public class NewChildWizardSession
    {
        [Key]
        public int SessionId { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string Username { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string ToddlerSession { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Stop { get; set; }
        public bool Complete { get; set; }

        public NewChildWizardSession()
        {

        }

    }

    public class EditChildWizardSession
    {

    }
}