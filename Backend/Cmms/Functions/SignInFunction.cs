using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Cmms.Core;
using Cmms.Core.Domain;
using Cmms.Presentation;
using MediatR;
using Cmms.Core.Application.Commands;
using Cmms.Core.Application.Models;

namespace Cmms
{
    public class SignInFunction : FunctionBase
    {

        private readonly IMediator _mediator;
        public SignInFunction(IMediator mediator)
        {
            this._mediator = mediator;
        }


        [FunctionName("SignIn")]
        public async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "signin")] SignInUserCommand model)
        {

            return await ExecuteAsync(async () =>
            {
                var result = await _mediator.Send(model);
                return OkResponse(new { id = result });

            });
            

        }


       
    }

 
    
}
