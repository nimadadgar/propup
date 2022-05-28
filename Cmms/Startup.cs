//using Cmms;
//using Cmms.Core;
//using Cmms.Core.Application;
//using Cmms.Infrastructure.Context;
//using Cmms.Infrastructure.Middleware;
//using FluentValidation;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.Azure.Functions.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Identity.Web;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
//using Microsoft.Azure.WebJobs.Host.Bindings;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;

//[assembly: FunctionsStartup(typeof(Cmms.Startup))]

//namespace Cmms
//{
//    public class Startup : FunctionsStartup
//    {

//        IConfiguration Configuration { get; set; }


//        public override void Configure(IFunctionsHostBuilder builder)
//        {


//            var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
//            var executionContextOptions = builder.Services.BuildServiceProvider()
//             .GetService<IOptions<ExecutionContextOptions>>().Value;


//            var currentDirectory = executionContextOptions.AppDirectory;

//            Configuration = new ConfigurationBuilder()
//               .SetBasePath(currentDirectory)
//               .AddConfiguration(configuration) // Add the original function configuration 
//               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//               .Build();

//            var config =  builder.GetContext().Configuration;
//            builder.Services.AddSingleton(Configuration);





//            ConfigureServices(builder.Services);




//        }


//        private void ConfigureServices(IServiceCollection services)
//        {

//            services.AddApplicationInsightsTelemetry(Configuration.GetSection("ApplicationInsights"));

//            services.AddDbContext<ApplicationContext>();
//            services.AddScoped<IUnitOfWork, ApplicationContext>();

//            /* Mediator Disable
//            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(EventLoggerBehavior<,>));
//            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
//           builder.Services.AddTransient<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();

//            */


//            services.AddScoped<IUserService, UserService>();


//            using (ApplicationContext ctx = new ApplicationContext())
//            {
//                //       ctx.Database.EnsureCreated();
//            }





//            //services.AddAuthentication(sharedOptions =>
//            //{
//            //    sharedOptions.DefaultScheme = Microsoft.Identity.Web.Constants.Bearer;
//            //    sharedOptions.DefaultChallengeScheme = Microsoft.Identity.Web.Constants.Bearer;
//            //})
//            //    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));
//        }


//    }
//}


