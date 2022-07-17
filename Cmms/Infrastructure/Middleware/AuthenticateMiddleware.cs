using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Cmms.Core.Application;
using Cmms.Core.Application.Exceptions;
using Cmms.Core.Application.Services;
using Cmms.Functions;
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




    public class AuthenticateMiddleware : IFunctionsWorkerMiddleware
    {

        private readonly IGraphService _graphService;
        private readonly IUserService _userService;

        public AuthenticateMiddleware(IGraphService graphService, IUserService userService)
        {
            _graphService = graphService;
            _userService = userService;
        }
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {

            (Type featureType, object featureInstance) = context.Features.SingleOrDefault(x => x.Key.Name == "IFunctionBindingsFeature");
                var inputData = featureType.GetProperties().SingleOrDefault(p => p.Name == "InputData")?.GetValue(featureInstance) as IReadOnlyDictionary<string, object>;
                var requestData = inputData?.Values.SingleOrDefault(obj => obj is HttpRequestData) as HttpRequestData;
            var accessor = context.InstanceServices.GetRequiredService<IClaimsPrincipalAccessor>();

          var isHeaderExist=  requestData.Headers.TryGetValues("x-ms-client-principal-id", out var header);
            if (!isHeaderExist)
                throw new BadRequestException("parameters is wrong");



            var users = await _graphService.GetAllUsers();


            var data = header.First();
                    var user = await _graphService.GetUserById(data);
            if (user is null)
                throw new BadRequestException("parameters is wrong");

                
                    List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.userId.FixNull()),
                    new Claim(ClaimTypes.Email,user.email.FixNull()),
                    new Claim(ClaimTypes.GivenName,user.displayName.FixNull()),
                    new Claim(ClaimTypes.Role,String.IsNullOrWhiteSpace(user.role)==true?"admin":user.role),
                };

                ClaimsIdentity clientIdentity = new ClaimsIdentity(claims,"Basic");
                accessor.Principal = new ClaimsPrincipal(clientIdentity);
            await next(context);

        }
    }

}
