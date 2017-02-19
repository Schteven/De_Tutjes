using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    [Table("Employees")]
    class Employee : Person
    {
        public int EmployeeID { get; set; }
        public int LocationID { get; set; }
        public int Role { get; set; }

        public Employee()
        {

        }
    }
}
