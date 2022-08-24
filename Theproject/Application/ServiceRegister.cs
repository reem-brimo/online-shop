using Application.OrdersAdmin;
using Application.UsersAdmin;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            //1.rename for add all services

            // 2.create a repositry pattern for orders and each indvial method call 
            // indvual service

            //3. edit razor pages to use from services but needs
            //to add http context to dependency inj
            @this.AddTransient<GetOrder>();
            @this.AddTransient<GetOrders>();
            @this.AddTransient<UpdateOrder>();
    
            @this.AddTransient<CreateUser>();
            return @this;

        }
    }
}
