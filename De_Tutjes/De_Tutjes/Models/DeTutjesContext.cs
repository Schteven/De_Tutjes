using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    public class DeTutjesContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public System.Data.Entity.DbSet<De_Tutjes.Models.Toddler> Toddlers { get; set; }
    }
}