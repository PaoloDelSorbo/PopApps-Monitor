using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PopApps.Monitor.Models.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}