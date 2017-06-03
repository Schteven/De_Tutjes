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
        [Display(Name = "Speciaal diëet")]
        public string SpecialDiet { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Allergieën")]
        public string Allergies { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Mag niet eten")]
        public string MayNotEat { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Flesvoeding")]
        public string BottlePowder { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Flessen per dag")]
        public int? BottleDay { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Speciale opmerking")]
        public string SpecialNotice { get; set; }

        public virtual ICollection<Toddler> Toddlers { get; set; }

        public Food()
        {

        }
    }
}
