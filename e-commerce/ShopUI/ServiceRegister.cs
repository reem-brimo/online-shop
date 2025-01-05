using Application;
using DataBase;
using Domain.Infrastructure;
using ShopUI.Infrastructure;
using System.Linq;
using System.Reflection;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            //1.rename for add all services


            //3. edit razor pages to use from services but needs
            //to add http context to dependency inj

            var serviceType = typeof(Service);
            var definedTypes = serviceType.Assembly.DefinedTypes;

            var services = definedTypes
                .Where(x => x.GetTypeInfo().GetCustomAttribute<Service>() != null);

            foreach (var service in services)
            {
                @this.AddTransient(service);
            }

            @this.AddTransient<IStockManager, StockManager>();
            @this.AddTransient<IProductManager, ProductManager>();
            @this.AddTransient<IOrderManager, OrderManager>();
            @this.AddScoped<ISessionManager, SessionManager>();

            return @this;

        }
    }
}
