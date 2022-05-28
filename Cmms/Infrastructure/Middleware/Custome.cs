using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Cmms.Core.Application.Exceptions;
using Cmms.Core.Application.Services;
using Cmms.Infrastructure.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cmms.Infrastructure.Middleware
{
    public interface IClaimsPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; set; }
      
    }
    
    public class ClaimsPrincipalAccessor : IClaimsPrincipalAccessor
    {
        private readonly ContextHolder _context = new();

        public ClaimsPrincipal Principal
        {
            get => _context.Context;
            set
            {
                
            _context.Context =   value ;
            }
        }

        private class ContextHolder
        {
            public ClaimsPrincipal Context;
        }
    }




    public class ClaimsPrincipalMiddleware : IFunctionsWorkerMiddleware
    {

        private readonly IGraphService _graphService;
        public ClaimsPrincipalMiddleware(IGraphService graphService)
        {
            _graphService  = graphService;
        }
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            // determine the type, the default is Microsoft.Azure.Functions.Worker.Context.Features.GrpcFunctionBindingsFeature
            (Type featureType, object featureInstance) = context.Features.SingleOrDefault(x => x.Key.Name == "IFunctionBindingsFeature");

            // find the input binding of the function which has been invoked and then find the associated parameter of the function for the data we want
            var inputData = featureType.GetProperties().SingleOrDefault(p => p.Name == "InputData")?.GetValue(featureInstance) as IReadOnlyDictionary<string, object>;
            var requestData = inputData?.Values.SingleOrDefault(obj => obj is HttpRequestData) as HttpRequestData;


            if (requestData.Headers.TryGetValues("x-ms-client-principal-id", out var header))
            {
                var accessor = context.InstanceServices.GetRequiredService<IClaimsPrincipalAccessor>();
                var data = header.First();
                var user = await _graphService.GetUserById(data);


                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.userId),
                    new Claim(ClaimTypes.GivenName,user.displayName),
                };

                ClaimsIdentity clientIdentity = new ClaimsIdentity(claims, "Basic");
                accessor.Principal = new ClaimsPrincipal(clientIdentity);

            }
            else
            {
                throw new AuthorizeException("user is not exist");
            }

            
            await next(context);
        }
    }

}
