using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Application.Abstractions;
using ShopBridge.Application.Data;
using System.Linq;
using System.Reflection;

namespace ShopBridge.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<IApplicationDbContext, ShopBridgeContext>();

            foreach (var repository in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Name.EndsWith("Repository") && x.IsClass))
            {
                foreach (var _interface in repository.GetInterfaces())
                {
                    services.AddScoped(_interface, repository);
                }
            }

            return services;
        }
    }
}
