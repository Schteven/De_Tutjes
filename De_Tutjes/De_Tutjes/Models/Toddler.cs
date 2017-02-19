using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    public class Toddler : Person
    {
        public int ToddlerId { get; set; }
        public int FoodId { get; set; }
        public int MedicalId { get; set; }
        public int SleepID { get; set; }
        public string ImportantNotice { get; set; }
        public string DailyRoutine { get; set; }

        public Toddler()
        {

        }
    }
}
