using System.Collections.Generic;
using System.Net;
using Cmms.Core.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Cmms.Infrastructure.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Cmms.Infrastructure.Middleware;

namespace Cmms
{

    [Authorize]
    public class GetUserInfo
    {
        private readonly ILogger _logger;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public GetUserInfo(ILoggerFactory loggerFactory, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _claimsPrincipalAccessor = claimsPrincipalAccessor;

            _logger = loggerFactory.CreateLogger<GetUserInfo>();
        }
        [Function("getuserinfo")]
        public  HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get",

Route = "getuserinfo")] HttpRequestData req,
       FunctionContext executionContext)
        {

           _logger.LogInformation( Newtonsoft.Json.JsonConvert.SerializeObject( req.Headers));
            var currentUser = _claimsPrincipalAccessor.Principal.UserInfo();
            return req.OkResponse(new { displayName = currentUser.displayName, id = currentUser.userId });



            
        
        }
    }
}
