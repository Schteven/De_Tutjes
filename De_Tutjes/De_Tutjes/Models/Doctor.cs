using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    [Table("Doctors")]
    public class Doctor : Person
    {
        public int DoctorId { get; set; }
        public int Title { get; set; }

        public virtual ICollection<Toddler> Toddlers { get; set; }
    }
}