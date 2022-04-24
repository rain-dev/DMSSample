// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pinewood.DMSSample.Business.Clients;
using Pinewood.DMSSample.Business.Extensions.DependencyInjection;
using Pinewood.DMSSample.Data.Extensions.DependencyInjection;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureDefaults(args)
    .ConfigureLogging(logging => logging.AddConsole())
    .ConfigureServices((ctx, services) =>
    {
        services.AddData(ctx.Configuration);
        services.AddService();
    })
    .Build();

await host.StartAsync();

using var scope = host.Services.CreateScope();

using var dmsClient = scope.ServiceProvider.GetRequiredService<IDMSClient>();

await dmsClient.CreatePartInvoiceAsync("1234", 10, "John Doe");
