using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{

    /*
    This table holds the Invoice
    */
    [Table("Invoices")]
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [ForeignKey("Toddler")]
        public int ToddlerId { get; set; }
        public virtual Toddler Toddler { get; set; }
        
        public int Month { get; set; }

        public int Year { get; set; }

        public int NormalDaysNextMonth { get; set; }

        public int ExtraDaysThisMonth { get; set; }

        public int DayCareClosedThisMonth { get; set; }

        public Boolean HasSibling { get; set; }

        public double TotalAmount { get; set; }

        public Boolean Payed { get; set; }

        public string OGM { get; set; }

        public DateTime CreationDate { get; set; }

        public string EmailSendToAddress { get; set; }

        public DateTime Emailsend { get; set; }
    }
}