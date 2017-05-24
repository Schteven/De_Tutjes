using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    [Table("AgreedDays")]
    public class AgreedDays
    {
        [Key]
        public int AgreedDaysId { get; set; }

        [Display(Name = "maandag")]
        public bool Monday { get; set; }
        [Display(Name = "dinsdag")]
        public bool Tuesday { get; set; }
        [Display(Name = "woensdag")]
        public bool Wednesday { get; set; }
        [Display(Name = "donderdag")]
        public bool Thursday { get; set; }
        [Display(Name = "vrijdag")]
        public bool Friday { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string SpecialNotice { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("Toddler")]
        public int ToddlerId { get; set; }
        public virtual Toddler Toddler { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }


    }
}