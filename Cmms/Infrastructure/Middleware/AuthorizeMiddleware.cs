using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
  
    public class AuthorizeMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public AuthorizeMiddleware(IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {

            //string functionEntryPoint = context.FunctionDefinition.EntryPoint;
            //Type assemblyType = Type.GetType(functionEntryPoint.Substring(0, functionEntryPoint.LastIndexOf('.')));
            //if (assemblyType == null)
            //{
            //    ///Process if other function called
            //}
            //else
            //{
            //    MethodInfo methodInfo = assemblyType.GetMethod(functionEntryPoint.Substring(functionEntryPoint.LastIndexOf('.') + 1));
            //    var attr = methodInfo?.GetCustomAttributes(typeof(ClaimAttribute), true).FirstOrDefault() as ClaimAttribute;
            //    if (attr != null)
            //    {
            //        ////////////////Check is Authinticate and Role Base
            //        if (_claimsPrincipalAccessor.Principal.Identity.IsAuthenticated == false)
            //            throw new AuthorizeException("User has not been authenticated");

            //        if (attr._claimType != String.Empty)
            //        {
            //            //////Check Claim
            //            if (!_claimsPrincipalAccessor.Principal.IsClaimExist(attr._claimType, attr._claim))
            //                throw new AuthorizeException("User Not Authorize");
            //        }

            //    }
            //}


            await next(context);

        }
    }

}
