using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DisplayFormat(NullDisplayText = "")]
        public string SpecialDiet { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string Allergies { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string MayNotEat { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string BottlePowder { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public int? BottleDay { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string SpecialNotice { get; set; }

        public virtual ICollection<Toddler> Toddlers { get; set; }

        public Food()
        {

        }
    }
}
