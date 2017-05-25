using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Display(Name = "Straat")]
        [DisplayFormat(NullDisplayText = "")]
        public string Street { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Nummer")]
        public int? Number { get; set; }
        [Display(Name = "Bus")]
        [DisplayFormat(NullDisplayText = "")]
        public string Bus { get; set; }
        [Display(Name = "Gemeente")]
        [DisplayFormat(NullDisplayText = "")]
        public string City { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Postcode")]
        public int? PostalCode { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}