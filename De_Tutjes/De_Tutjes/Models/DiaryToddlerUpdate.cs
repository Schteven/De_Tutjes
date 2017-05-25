using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    //This table holds the updates of the Toddlers
    //Updates:
    //1 = CheckIn | Child arrived at daycare
    //2 = Sleeping| Child is currently sleeping
    //3 = WakeUp | Child is Awake
    //4 = Eating | Child is Eating (food info in comment)
    //5 = Diaper | Child diaper change
    //6 = CheckOut | Child leaving daycare
    //7 = Comment | Random Comment throughout the day
    //At the end of the day, when closing the application, the parent can receive an email with the history of today


    [Table("DiaryToddlerUpdates")]
    public class DiaryToddlerUpdate
    {
        [Key]
        public int DiaryToddlerUpdateId { get; set; }

        public DateTime Timestamp { get; set; }

        [ForeignKey("Toddler")]
        public int ToddlerId { get; set; }
        public virtual Toddler Toddler { get; set; }

        public int UpdateType { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public String Comment { get; set; }
    }
}