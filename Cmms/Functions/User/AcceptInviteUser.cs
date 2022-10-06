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
using Cmms.Infrastructure.Dto;
using Cmms.Core.Application;

namespace Cmms
{

    public class AcceptInviteUser
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public AcceptInviteUser(ILoggerFactory loggerFactory,
            IUserService userService  )
        {
            _logger = loggerFactory.CreateLogger<InviteUser>();
            _userService = userService;

        }




        [Function("AcceptInvite")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post",Route = "user/acceptinvite")] HttpRequestData req,
       FunctionContext executionContext)
        {

            var requestObject = await req.GetBodyAsync<AcceptInviteUserDto>();
            if (!requestObject.IsValid)
            {
                return req.BadResponse("Required some field please fill in", HttpStatusCode.BadRequest, requestObject.ValidationResults);
            }

            var result = await _userService.AcceptInvitedUser( requestObject.Value.Id,requestObject.Value.Password);
            return req.OkResponse(new {  });

        
        }
    }
}
