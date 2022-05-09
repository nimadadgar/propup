using Cmms;
using Cmms.Core;
using Cmms.Core.Application;
using Cmms.Core.Application.Behaviors;
using Cmms.Core.Application.Commands;
using Cmms.Infrastructure.Context;
using Cmms.Infrastructure.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: FunctionsStartup(typeof(Cmms.Startup))]

namespace Cmms
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMediatR(typeof(Startup));

            builder.Services.AddDbContext<ApplicationContext>();
            builder.Services.AddScoped<IUnitOfWork, ApplicationContext>();


            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(EventLoggerBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
           builder.Services.AddTransient<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();


            builder.Services.AddScoped<IUserService, UserService>();


           using (ApplicationContext ctx=new ApplicationContext())
            {
         //       ctx.Database.EnsureCreated();
            }




        }
    }
}
