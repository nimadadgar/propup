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
using Microsoft.OpenApi.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Cmms.Functions;

namespace Cmms
{

    public class GetUserInfo
    {
        private readonly ILogger _logger;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public GetUserInfo(ILoggerFactory loggerFactory, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _logger = loggerFactory.CreateLogger<GetUserInfo>();
        }



        //[OpenApiOperation(operationId: "greeting", tags: new[] { "greeting" }, Summary = "Greetings", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
        //[OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "The response", Description = "This returns the response")]
       
        [Function("getuserinfo")]
        [ClaimAttribute(ClaimTypes.Role, "admin")]
        [ClaimAttribute(ClaimTypes.Role, "user")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get",Route = "getuserinfo")] HttpRequestData req,
       FunctionContext executionContext)
        {

           _logger.LogInformation( Newtonsoft.Json.JsonConvert.SerializeObject( req.Headers));
            var currentUser = _claimsPrincipalAccessor.Principal.UserInfo();
            return req.OkResponse(new { displayName = currentUser.displayName, id = currentUser.userId });



            
        
        }
    }
}
