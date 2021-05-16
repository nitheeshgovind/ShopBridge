using ShopBridge.WebApi.Filters;
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

            // Initialize dependency injection
            DependencyConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}
