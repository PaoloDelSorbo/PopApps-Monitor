using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PopApps.Monitor.Models.Domain;
using PopApps.Monitor.Models.EntityConfigurations;
using System.Web.Security;

namespace PopApps.Monitor.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Fan> Fans { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FanEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        internal static MyDbContext Create()
        {
            var http = HttpContext.Current;
            if (http.Items["MyDbContext"] == null)
                http.Items["MyDbContext"] = new MyDbContext();
            return http.Items["MyDbContext"] as MyDbContext;
        }
    }

    //public class MyDatabaseInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    //{
    //    protected override void Seed(MyDbContext context)
    //    {
    //        base.Seed(context);
    //    }
    //}
}