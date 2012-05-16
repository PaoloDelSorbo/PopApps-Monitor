using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using PopApps.Monitor.Models.DAL;
using PopApps.Monitor.Models.Domain;
using System;
namespace PopApps.Monitor.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            FansRepository repository = new FansRepository(DbContext);
            Fan myFan = new Fan();
            myFan.FirstName = "Paolo";
            myFan.LastName = "Del Sorbo";
            repository.InsertOrUpdate(myFan);
            repository.SaveChanges();
            
            List<Fan> fans;
            fans = repository.All.ToList();


            return View(fans);
        }
        public ActionResult About()
        {
            return View();
        }
        
    }
}
