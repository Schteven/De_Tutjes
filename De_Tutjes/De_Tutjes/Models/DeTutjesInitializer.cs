using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace De_Tutjes.Models
{
    public class DeTutjesInitializer : DropCreateDatabaseIfModelChanges<DeTutjesContext>
    {
        protected override void Seed(DeTutjesContext context)
        {
            base.Seed(context);
        }
    }
}