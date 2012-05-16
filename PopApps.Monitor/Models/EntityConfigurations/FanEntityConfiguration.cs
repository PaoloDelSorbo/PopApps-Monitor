using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using PopApps.Monitor.Models.Domain;

namespace PopApps.Monitor.Models.EntityConfigurations
{
    public class FanEntityConfiguration : EntityTypeConfiguration<Fan>
    {
        public FanEntityConfiguration()
        {
            ToTable("Fans");
        }
    }
}