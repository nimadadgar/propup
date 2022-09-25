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

    public class MemberList
    {
        private readonly ILogger _logger;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IUserService _userService;

        public MemberList(ILoggerFactory loggerFactory,
            IUserService userService,
            IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _logger = loggerFactory.CreateLogger<InviteUser>();
            _userService = userService;

        }




        [Function("MemberList")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post",Route = "user/list")] HttpRequestData req,
       FunctionContext executionContext)
        {


            var result = await _userService.UserList();
            return req.OkResponse(new { list = result });

        
        }
    }
}
