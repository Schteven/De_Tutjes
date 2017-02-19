using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Models
{
    class Parent : Person
    {
        public int ParentID { get; set; }
        public string Relation { get; set; } // Mother or father

        public Parent()
        {

        }
    }
}
