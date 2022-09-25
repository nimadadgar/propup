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

    public class InitialInviteUser
    {
        private readonly ILogger _logger;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly GeneralService _generalService;

        public InitialInviteUser(ILoggerFactory loggerFactory,
            GeneralService generalService,
            IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _logger = loggerFactory.CreateLogger<InitialInviteUser>();
            _generalService = generalService;

        }




        [Function("InitialInvite")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get",Route = "user/initialInvite")] HttpRequestData req,
       FunctionContext executionContext)
        {
            return req.OkResponse(new
            {
                factories = (await _generalService.GetFactories()),
                   jobs = await _generalService.GetJobs(),
                accessLevels = (await _generalService.GetAccessLevels()).Select(d => new { accessLevelName = d.AccessLevelName }).ToList()
            }) ;

        }
    }
}
