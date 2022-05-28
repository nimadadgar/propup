//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Microsoft.Azure.Functions.Worker;

//namespace Cmms.Functions
//{
//    public static class TestRequest
//    {
//        [FunctionName("TestRequest")]
//        public static async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
//            ILogger log)
//        {
//            log.LogInformation("222 C# Application Insight Log Test");
//            log.LogInformation("222 this is a information message...");
//            log.LogWarning("222 this is a warning message...");

//            String name = "";
          

//            string responseMessage = $"Hello, {name} with User ID. This is Test Level function for authorizing";

//            return new OkObjectResult(responseMessage);
//        }
//    }
//}
