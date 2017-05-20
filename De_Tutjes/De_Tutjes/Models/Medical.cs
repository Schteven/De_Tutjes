using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("MedicalInfo")]
    public class Medical
    {
        public int MedicalID { get; set; }
        [Display(Name = "Rijksregisternummer")]
        public string HealthServiceNumber { get; set; }
        [Display(Name = "Medicatie")]
        public bool Medication { get; set; }
        [Display(Name = "Medicatie")]
        public string MedicationName { get; set; }
        [Display(Name = "Allergieën")]
        public string Allergies { get; set; }
        [Display(Name = "Allergiemedicatie")]
        public string AllergiesMedication { get; set; }
        [Display(Name = "Kinderziekte")]
        public string ChildDisease { get; set; }
        [Display(Name = "Wanneer?")]
        public string DiseaseWhen { get; set; }
        [Display(Name = "Speciale opmerking")]
        public string SpecialNotice { get; set; }

        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public virtual ICollection<Toddler> Toddlers { get; set; }

        public Medical()
        {

        }
    }
}
