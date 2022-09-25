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

    public class GetInviteUserInfo
    {
        private readonly ILogger _logger;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IUserService _userService;

        public GetInviteUserInfo(ILoggerFactory loggerFactory,
            IUserService userService,
            IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _logger = loggerFactory.CreateLogger<GetInviteUserInfo>();
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _userService = userService;
        }




        [Function("inviteinfo")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get",Route = "user/inviteinfo/{id}")] 
        HttpRequestData req,
            Guid id,
       FunctionContext executionContext)
        {

            var result = await _userService.ProcessGetInviteUser( id);
            if (result == null)
                return req.BadResponse("Invited Link Expired");



            return req.OkResponse("OK");

        
        }
    }
}
