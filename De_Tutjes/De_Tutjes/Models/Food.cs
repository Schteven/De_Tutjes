using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    public class Food
    {
        public int FoodID { get; set; }
        public string SpecialDiet { get; set; }
        public string Allergies { get; set; }
        public string MayNotEat { get; set; }
        public string BottlePowder { get; set; }
        public int BottleDay { get; set; }
        public string SpecialNotice { get; set; }

        public Food()
        {

        }
    }
}
