using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    [Table("Locations")]
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string Name { get; set; }

        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }
}