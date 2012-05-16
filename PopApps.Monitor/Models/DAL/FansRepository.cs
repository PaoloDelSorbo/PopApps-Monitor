using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PopApps.Monitor.Models;
using PopApps.Monitor.Models.Domain;

namespace PopApps.Monitor.Models.DAL
{
    public class FansRepository : BaseRepository<Fan>
    {
        public FansRepository(MyDbContext context) :base(context){}

        

    }
}