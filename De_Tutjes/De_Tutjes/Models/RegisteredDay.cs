using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    [Table("RegisteredDays")]
    public class RegisteredDay
    {
        [Key]
        public int RegisteredDayId { get; set; }
        
        [ForeignKey("Toddler")]
        public int ToddlerId { get; set; }
        public virtual Toddler Toddler { get; set; }

        public DateTime DayInDaycare { get; set; }

        public Boolean CheckedIn { get; set; }

        public Boolean ExtraDay { get; set; }
        
        public Boolean DaycareIsClosed { get; set; }
                
    }
}
