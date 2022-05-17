using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using WCF.Data.Models;

namespace WCF.Data
{
    public class ResourceContext : DbContext
    {
        public ResourceContext():base("ResourceContext"){
            Database.SetInitializer(new CreateDatabaseIfNotExists<ResourceContext>());
        }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
