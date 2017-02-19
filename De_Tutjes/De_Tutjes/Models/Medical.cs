using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    class Medical
    {
        public int MedicalID { get; set; }
        public int DoctorID { get; set; }
        public string HealthServiceNumber { get; set; }
        public bool Medication { get; set; }
        public string MedicationName { get; set; }
        public string Allergies { get; set; }
        public string AllergiesMedication { get; set; }
        public string ChildDisease { get; set; }
        public string DiseaseWhen { get; set; }
        public string SpecialNotice { get; set; }

        public Medical()
        {

        }
    }
}
