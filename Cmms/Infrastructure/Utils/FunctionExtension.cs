using Cmms.Core.Application.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Utils
{
    internal static class FunctionUtilities
    {
        internal static HttpRequestData GetHttpRequestData(this FunctionContext context)
        {
            var keyValuePair = context.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
            var functionBindingsFeature = keyValuePair.Value;
            var type = functionBindingsFeature.GetType();
            var inputData = type.GetProperties().Single(p => p.Name == "InputData").GetValue(functionBindingsFeature) as IReadOnlyDictionary<string, object>;
            return inputData?.Values.SingleOrDefault(o => o is HttpRequestData) as HttpRequestData;
        }

        internal static void InvokeResult(this FunctionContext context, HttpResponseData response)
        {
            var keyValuePair = context.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
            var functionBindingsFeature = keyValuePair.Value;
            var type = functionBindingsFeature.GetType();
            var result = type.GetProperties().Single(p => p.Name == "InvocationResult");
            result.SetValue(functionBindingsFeature, response);
        }


        public static HttpResponseData BadResponse(this HttpRequestData req, string message)
        {
            return BadResponse(req, message, HttpStatusCode.BadRequest, null);
        }
        public static HttpResponseData BadResponse(this HttpRequestData req, string message, HttpStatusCode statusCode)
        {
            return BadResponse(req, message, statusCode, null);
        }
        public static HttpResponseData BadResponse(this HttpRequestData req, string message, HttpStatusCode statusCode, object data)
        {
            BadResponseViewModel responseModel = new BadResponseViewModel(message,
                 Guid.NewGuid().ToString(), data);
            var response = req.CreateResponse();
            response.WriteAsJsonAsync(responseModel, (HttpStatusCode)statusCode);

            return response;


        }

        public static HttpResponseData OkResponse(this HttpRequestData req, object data)
        {
            ResponseViewModel responseModel = new ResponseViewModel(data);
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(responseModel);
            return response;
        }
    }
}
