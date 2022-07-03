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
    public class AddSparePart
    {
        private readonly ILogger _logger;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IEquipmentService _equipmentService;

        public AddSparePart(ILoggerFactory loggerFactory,
            IEquipmentService equipmentService,
            IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _logger = loggerFactory.CreateLogger<AddEquipment>();
            _equipmentService = equipmentService;
        }

       [Function("addSparePart")]
        [ClaimAttribute(ClaimTypes.Role, "admin")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post",Route = "addSparePart")]
        HttpRequestData req,
            FunctionContext executionContext)
        {

           var requestObject=await req.GetBodyAsync<AddSparePartDto>();
            if (!requestObject.IsValid)
            {
                return req.BadResponse("Required some field please fill in", HttpStatusCode.BadRequest, requestObject.ValidationResults);
            }


            var result = await  _equipmentService.AddSparePart(requestObject.Value);
            return req.OkResponse(new { result.Id });

        }
    }
}
