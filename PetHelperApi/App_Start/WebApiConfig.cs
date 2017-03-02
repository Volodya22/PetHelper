using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using DataContext;
using Microsoft.Practices.Unity;
using PetHelperApi.Models;
using PetHelperApi.Services;
using RouteParameter = System.Web.Http.RouteParameter;

namespace PetHelperApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IRepository, PetHelperContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IMail, MailService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
