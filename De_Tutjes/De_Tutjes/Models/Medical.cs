using System;
using System.Collections.Generic;
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
        
        public string HealthServiceNumber { get; set; }
        public bool Medication { get; set; }
        public string MedicationName { get; set; }
        public string Allergies { get; set; }
        public string AllergiesMedication { get; set; }
        public string ChildDisease { get; set; }
        public string DiseaseWhen { get; set; }
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
