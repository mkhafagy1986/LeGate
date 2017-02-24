using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HotelsService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetAllHotelsApi",
                routeTemplate: "api/{controller}/{Action}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "SearchApi",
                routeTemplate: "api/{controller}/{SearchFieldName}/{SerachFieldValue}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
