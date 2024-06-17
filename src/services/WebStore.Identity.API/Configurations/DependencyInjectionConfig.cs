using WebStore.Business.Interfaces;
using WebStore.Business.Notifications;
using WebStore.Business.Services;
using WebStore.Data.Repository;

namespace WebStore.Identity.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Business
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<INotifier, Notificator>();

            return services;
        }
    }
}
