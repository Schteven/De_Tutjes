using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("Eating")]
    public class Food
    {
        public int FoodID { get; set; }
        public string SpecialDiet { get; set; }
        public string Allergies { get; set; }
        public string MayNotEat { get; set; }
        public string BottlePowder { get; set; }
        public int? BottleDay { get; set; }
        public string SpecialNotice { get; set; }

        public virtual ICollection<Toddler> Toddlers { get; set; }

        public Food()
        {

        }
    }
}
