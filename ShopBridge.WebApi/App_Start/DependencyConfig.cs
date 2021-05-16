using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Application;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace ShopBridge.WebApi
{
    public class DependencyConfig
    {
        public static void Initialize(HttpConfiguration config)
        {

            IServiceCollection services = new ServiceCollection();

            foreach (var item in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Name.EndsWith("Controller")))
            {
                services.AddTransient(item);
            }

            services.AddApplication();

            config.DependencyResolver = new DependencyResolver(services.BuildServiceProvider());

        }
    }
}