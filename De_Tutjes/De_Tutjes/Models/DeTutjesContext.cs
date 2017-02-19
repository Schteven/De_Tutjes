using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    public class DeTutjesContext : DbContext
    {
        // Person info
        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }

        // Toddlers info
        public DbSet<RelationLink> RelationLinks { get; set; }
        public DbSet<Food> Eating { get; set; }
        public DbSet<Sleep> Sleeping { get; set; }
        public DbSet<Medical> Medical { get; set; }
    }
}