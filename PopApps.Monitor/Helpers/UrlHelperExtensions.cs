using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PopApps.Monitor.Helpers
{
    public static class UrlHelperExtensions
    {
        public static string CdnContent(this UrlHelper urlHelper, string contentPath)
        {
            return ConfigHelper.CdnUrl + urlHelper.Content(contentPath);
        }

        internal static Uri BaseUri(this UrlHelper urlHelper)
        {
            return new Uri("http://" + urlHelper.RequestContext.HttpContext.Request.Headers["Host"]);
        }
        internal static Uri ActionFull(this UrlHelper urlHelper, string actionName)
        {
            return new Uri(BaseUri(urlHelper), urlHelper.Action(actionName));
        }
        internal static Uri ActionFull(this UrlHelper urlHelper, string actionName, string controllerName)
        {
            return new Uri(BaseUri(urlHelper), urlHelper.Action(actionName, controllerName));
        }

        internal static Uri ActionFull(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues)
        {
            return new Uri(BaseUri(urlHelper), urlHelper.Action(actionName, controllerName, routeValues));
        }

        internal static Uri ActionFull(this UrlHelper urlHelper, string actionName, object routeValues)
        {
            return new Uri(BaseUri(urlHelper), urlHelper.Action(actionName, routeValues));
        }
    }
}