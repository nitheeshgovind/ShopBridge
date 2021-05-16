using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Application;
using ShopBridge.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShopBridge.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Filters.Add(new ExceptionFilterAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new ValiadationFilter());

            IServiceCollection services = new ServiceCollection();

            foreach (var item in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Name.EndsWith("Controller")))
            {
                services.AddTransient(item);
            }

            services.AddApplication();

            GlobalConfiguration.Configuration.DependencyResolver = new DependencyResolver(services.BuildServiceProvider());

        }
    }
}
