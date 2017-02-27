using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    [Table("Addresses")]
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Bus { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}