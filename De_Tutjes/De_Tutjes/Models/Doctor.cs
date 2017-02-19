using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    public class Doctor : Person
    {
        public int DoctorId { get; set; }
        public int Title { get; set; }
    }
}