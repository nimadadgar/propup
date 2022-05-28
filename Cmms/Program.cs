using Cmms.Core;
using Cmms.Infrastructure;
using Cmms.Infrastructure.Context;
using Cmms.Infrastructure.Middleware;
using Cmms.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Configuration;
using Cmms.Core.Application.Services;

public class Program
{
    private static IConfiguration Configuration { get; set; }

    static async Task Main(string[] args)
    {


        var host = new HostBuilder()
          .ConfigureAppConfiguration((hostContext, config) =>
          {
          })
             .ConfigureFunctionsWorkerDefaults(workerApplication =>
             {

                 // Register our custom middlewares with the worker
                 workerApplication.UseMiddleware<ExceptionHandlingMiddleware>();
                 workerApplication.UseMiddleware<ClaimsPrincipalMiddleware>();

             })
            .ConfigureServices((context, services) =>
            {
                services.AddLogging();

                services.AddSingleton<IClaimsPrincipalAccessor, ClaimsPrincipalAccessor>();

                String key = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");

                services.AddApplicationInsightsTelemetryWorkerService(key);
                services.AddScoped<IGraphService, GraphService>(sp =>
                    new GraphService(
                    Environment.GetEnvironmentVariable("Graph_TanentId"),
                    Environment.GetEnvironmentVariable("Graph_ClientId"),
                    Environment.GetEnvironmentVariable("Graph_SecreteKey")
                    )
                        );



                // logger.LogError(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY"));





                //services.AddApplicationInsightsTelemetry(opts =>
                //{
                //    opts.DependencyCollectionOptions.EnableLegacyCorrelationHeadersInjection = true;
                //    //opts.InstrumentationKey = key;

                //});


                //       services.AddApplicationInsightsTelemetry(key);


                //var configuration = context.Configuration;
                //AzureFunctionSettings model = new AzureFunctionSettings();



                services.AddDbContext<ApplicationContext>(opt =>
                {
                    opt.UseCosmos(
                Environment.GetEnvironmentVariable("COSMOS_ENDPOINT"),
                Environment.GetEnvironmentVariable("COSMOS_EACCOUNTKEY"),

                databaseName: "propupcmmsdb");

                });

                      services.AddScoped<IUnitOfWork, ApplicationContext>();


            })
            .Build();

        host.Run();

    }

}
