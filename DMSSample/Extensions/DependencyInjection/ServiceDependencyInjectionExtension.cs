using Microsoft.Extensions.DependencyInjection;
using Pinewood.DMSSample.Business.Clients;

namespace Pinewood.DMSSample.Business.Extensions.DependencyInjection
{
    public static class ServiceDependencyInjectionExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddHttpClient<IPartAvailabilityClient, PartAvailabilityClient>();
            services.AddScoped<IDMSClient, DMSClient>();
            services.AddScoped<IPartInvoiceController, PartInvoiceController>();

            return services;
        }
    }
}
