using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cmms.Core.Application.Exceptions;
using Cmms.Infrastructure.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace Cmms.Infrastructure.Middleware
{
    internal sealed class ExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                ILogger logger = context.GetLogger<ExceptionHandlingMiddleware>();
                var req = context.GetHttpRequestData();
                HttpResponseData responseData = null;

                if (ex  is Microsoft.Graph.ServiceException)
                {
                   if ((ex as Microsoft.Graph.ServiceException). StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        responseData = req.BadResponse(ex.Message, System.Net.HttpStatusCode.Unauthorized);
                    }
                }
                else if (ex is AuthorizeException)
                {
                    responseData = req.BadResponse(ex.Message, System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    responseData = req.BadResponse(ex.Message, System.Net.HttpStatusCode.InternalServerError);
                }

                logger.LogError("Error From function: {message} {error}", context.FunctionDefinition.Name,ex.Message);
                context.InvokeResult(responseData);

              
             
            }
        }
    }
}
