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
using Cmms.Core.Domain;

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
                 workerApplication.UseMiddleware<AuthenticateMiddleware>();
                 workerApplication.UseMiddleware<AuthorizeMiddleware>();
                 
               


             })
            .ConfigureServices((context, services) =>
            {


            

                services.AddControllers()
              .AddJsonOptions(options =>
          {
           options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
           });


                services.AddLogging();

                services.AddSingleton<IClaimsPrincipalAccessor, ClaimsPrincipalAccessor>();

                String key = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");

                services.AddApplicationInsightsTelemetryWorkerService(key);

                services.AddScoped<MailSender, MailSender>(sp =>
                    new MailSender(Environment.GetEnvironmentVariable("SendMail_ApiKey") )
                        );


                services.AddScoped<IGraphService, GraphService>(sp =>
                    new GraphService(
                    Environment.GetEnvironmentVariable("Graph_TanentId"),
                    Environment.GetEnvironmentVariable("Graph_ClientId"),
                    Environment.GetEnvironmentVariable("Graph_SecreteKey"),
                    Environment.GetEnvironmentVariable("Graph_B2cExtensionAppClientId")
                    )
                        );
                services.AddScoped<GeneralService>();

                services.AddScoped<IEquipmentService, EquipmentService>();
                services.AddScoped<ITeamService, TeamService>();
                services.AddScoped<IUserService, UserService>();

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


            //ApplicationContext ctx = new ApplicationContext(opt.Options);
          //  Cmms.InitialDb.InitialMetal(ctx);
            //ctx.SaveChanges();

        }
        catch (Exception er)
        {

        }





        host.Run();

    }

}
