using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public int HomePhone { get; set; }
        public int CellPhone { get; set; }
        public int WorkPhone { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }

        public Person()
        {

        }

    }
}
