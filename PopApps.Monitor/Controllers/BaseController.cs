using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PopApps.Monitor.Models;

namespace PopApps.Monitor.Controllers
{
    public class BaseController : Controller
    {
        public MyDbContext DbContext { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            DbContext = MyDbContext.Create();
            base.Initialize(requestContext);

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            base.OnActionExecuting(filterContext);
            filterContext.Controller.ViewBag.ApplicationName = ConfigurationManager.AppSettings["ApplicationName"];
            filterContext.Controller.ViewBag.Copyright = ConfigurationManager.AppSettings["Copyright"];
        }
    }
}
