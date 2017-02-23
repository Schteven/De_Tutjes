using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;

namespace De_Tutjes.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Person> Persons { get; set; }

    }

    public class DeTutjesContext : IdentityDbContext<ApplicationUser>
    {
        public DeTutjesContext() : base("DeTutjesContext")
        {
            Database.SetInitializer<DeTutjesContext>(null);// Remove default initializer
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static DeTutjesContext Create()
        {
            return new DeTutjesContext();
        }

        // Person info
        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }

        // Toddlers info
        public DbSet<RelationLink> RelationLinks { get; set; }
        public DbSet<Food> Eating { get; set; }
        public DbSet<Sleep> Sleeping { get; set; }
        public DbSet<Medical> Medical { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Conventions.Remove<PluralizingTableNameConvention>();
            builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }
}