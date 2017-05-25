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
        [DisplayFormat(NullDisplayText = "")]
        public string HealthServiceNumber { get; set; }
        [Display(Name = "Medicatie")]
        [DisplayFormat(NullDisplayText = "")]
        public bool Medication { get; set; }
        [Display(Name = "Medicatie")]
        [DisplayFormat(NullDisplayText = "")]
        public string MedicationName { get; set; }
        [Display(Name = "Allergieën")]
        [DisplayFormat(NullDisplayText = "")]
        public string Allergies { get; set; }
        [Display(Name = "Allergiemedicatie")]
        [DisplayFormat(NullDisplayText = "")]
        public string AllergiesMedication { get; set; }
        [Display(Name = "Kinderziekte")]
        [DisplayFormat(NullDisplayText = "")]
        public string ChildDisease { get; set; }
        [Display(Name = "Wanneer?")]
        [DisplayFormat(NullDisplayText = "")]
        public string DiseaseWhen { get; set; }
        [Display(Name = "Speciale opmerking")]
        [DisplayFormat(NullDisplayText = "")]
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
