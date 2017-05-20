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
        [Display(Name = "Speciaal diëet")]
        public string SpecialDiet { get; set; }
        [Display(Name = "Allergieën")]
        public string Allergies { get; set; }
        [Display(Name = "Mag niet eten")]
        public string MayNotEat { get; set; }
        [Display(Name = "Flesvoeding")]
        public string BottlePowder { get; set; }
        [Display(Name = "lessen per dag")]
        public int? BottleDay { get; set; }
        [Display(Name = "Speciale opmerking")]
        public string SpecialNotice { get; set; }

        public virtual ICollection<Toddler> Toddlers { get; set; }

        public Food()
        {

        }
    }
}
