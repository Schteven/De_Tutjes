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
        [DisplayFormat(NullDisplayText = "")]
        public string HealthServiceNumber { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public bool Medication { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string MedicationName { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string Allergies { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string AllergiesMedication { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string ChildDisease { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string DiseaseWhen { get; set; }
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
