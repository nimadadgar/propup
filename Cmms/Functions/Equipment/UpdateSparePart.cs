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
    public class UpdateSparePart
    {
        private readonly ILogger _logger;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IEquipmentService _equipmentService;

        public UpdateSparePart(ILoggerFactory loggerFactory,
            IEquipmentService equipmentService,
            IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _logger = loggerFactory.CreateLogger<CreateEquipment>();
            _equipmentService = equipmentService;
        }

       [Function("updatesparepart")]
        [ClaimAttribute(ClaimTypes.Role, "admin")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post",Route = "equipment/updatesparepart")]
        HttpRequestData req,
            FunctionContext executionContext)
        {

           var requestObject=await req.GetBodyAsync<UpdateSparePartDto>();
            if (!requestObject.IsValid)
            {
                return req.BadResponse("Required some field please fill in", HttpStatusCode.BadRequest, requestObject.ValidationResults);
            }

            var result = await  _equipmentService.UpdateSparePart(requestObject.Value);
            return req.OkResponse(new {id= result.Id });

        }
    }
}
