using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    //This table holds the status of the Toddlers
    //Status:
    //1 = Home | Child is at home
    //2 = Sleeping | Child is currently sleeping
    //3 = Normal | Child is at daycare and ready for updates
    //At the end of the day, when closing the application, the status should always be 1 (Home)
    //ToddlerId should be unique, there can be only one Row for each toddler.
    //Location is updatable
    

    [Table("DiaryToddlerStatuses")]
    public class DiaryToddlerStatus
    {
        [Key]
        public int DiaryToddlerStatusId { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        [ForeignKey("Toddler")]
        public int ToddlerId { get; set; }
        public virtual Toddler Toddler { get; set; }

        public int Status { get; set; }


        public DiaryToddlerStatus()
        {

        }

    }
}