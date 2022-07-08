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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Cmms.Infrastructure.Dto;
using Cmms.Core.Application;
using System.ComponentModel.DataAnnotations;
using Cmms.Functions;

namespace Cmms
{

    [Authorize]
    public class GetListEquipment
    {
        private readonly ILogger _logger;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IEquipmentService _equipmentService;

        public GetListEquipment(ILoggerFactory loggerFactory,
            IEquipmentService equipmentService,
            IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _logger = loggerFactory.CreateLogger<CreateEquipment>();
            _equipmentService = equipmentService;
        }

        [Function("getlist")]
        [ClaimAttribute(ClaimTypes.Role, "admin")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get",Route = "equipment/getlist")]
        HttpRequestData req,
            FunctionContext executionContext)
        {
            var result = await  _equipmentService.Get();
            return req.OkResponse(new {model= result });

        }
    }
}
