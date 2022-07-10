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
using System.Reflection;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Cmms.Core.Application;
using Microsoft.Azure.Functions.Worker.Configuration;

public class Program
{
    private static IConfiguration Configuration { get; set; }

    static async Task Main(string[] args)
    {


        var host = new HostBuilder()
          .ConfigureAppConfiguration((hostContext, config) =>
          {
          }).ConfigureOpenApi()
                          

             .ConfigureFunctionsWorkerDefaults(workerApplication =>
             {

                 // Register our custom middlewares with the worker
                 workerApplication.UseMiddleware<ExceptionHandlingMiddleware>();
                 workerApplication.UseMiddleware<AuthenticateMiddleware>();
                 workerApplication.UseMiddleware<AuthorizeMiddleware>();


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


                services.AddScoped<IEquipmentService, EquipmentService>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<ITeamService, TeamService>();


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

        try
        {
            var opt = new DbContextOptionsBuilder<ApplicationContext>();

            opt.UseCosmos(
                   Environment.GetEnvironmentVariable("COSMOS_ENDPOINT"),
                   Environment.GetEnvironmentVariable("COSMOS_EACCOUNTKEY"),
                   databaseName: "propupcmmsdb"
                );
            ApplicationContext ctx = new ApplicationContext(opt.Options);

            //var client = ctx.Database.GetCosmosClient();
            //var container = client.GetContainer("propupcmmsdb", "equipment");
            //await container.DeleteContainerAsync();

            //ctx.Database.EnsureDeleted();
            //ctx.Database.EnsureCreated();
            //await ctx.Users.AddAsync(new Cmms.Core.Domain.User
            //{
            //    Company = "zoser",
            //    Id = Guid.Parse("2f8f91e9-6298-4eae-9e0f-48a99500cd30"),
            //    FullName = "javad zobeidi",
            //    Email = "javad",
            //    Role = "admin"

            //});
            //ctx.SaveChanges();



            //ctx.Database.


        }
        catch (Exception er)
        {

        }
      


        host.Run();

    }

}
