using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace Pinewood.DMSSample.Data.Extensions.DependencyInjection
{
    public static class DmsDataDependencyInjectionExtensions
    {
        public static void AddData(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped<SqlCommand>(o => new()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = new SqlConnection(configuration.GetConnectionString("appDatabase"))
            });

            services.AddSingleton<SqlCommandFactory>();

            services.AddScoped<ICustomerRepositoryDB, CustomerRepositoryDB>();
            services.AddScoped<IPartInvoiceRepositoryDB, PartInvoiceRepositoryDB>();
        }
    }
}
