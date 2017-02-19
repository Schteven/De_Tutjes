using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("Toddlers")]
    public class Toddler : Person
    {
        public int ToddlerId { get; set; }
        public int FoodId { get; set; }
        public int MedicalId { get; set; }
        public int SleepID { get; set; }
        public string DailyRoutine { get; set; }
        public string ImportantNotice { get; set; }

        

        public Toddler()
        {

        }
    }
}
