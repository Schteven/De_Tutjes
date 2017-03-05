using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    [Table("Doctors")]
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public string Title { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        public virtual ICollection<Toddler> Toddlers { get; set; }
    }
}