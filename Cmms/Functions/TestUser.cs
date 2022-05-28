 //using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Microsoft.Graph;
//using Azure.Identity;
//using Microsoft.Azure.Functions.Worker;

//namespace Cmms.Functions
//{
//    public static class App2
//    {
//        public static string tanentId = "33bc5004-330b-4891-a363-ff31537d05e7";
//        public static string clientId = "64f977bf-a132-40fd-a897-01ceb9a788ee";
//        public static string client_secret = "3G~8Q~aptjo8Jzgyj--Yg6~jhIVbf7hC4jdNObe4";
//    }


//    public static class AppDefault
//    {
//        public static string tanentId = "dae7f97a-d74f-4f6f-9b7f-dcfb68a7a3fb";
//        public static string clientId = "06552eaf-c534-4872-b629-d191f56fc86c";
//        public static string client_secret = "GLG8Q~-mc2l2do37XWH5TlmCsDUxl6cFIP0FYc7P";
//    }


//    public static class ApiGraphManagement
//    {
//        public static string tanentId = "33bc5004-330b-4891-a363-ff31537d05e7";
//        public static string clientId = "2d74848e-be8a-4fbe-844d-87982272b3fb";
//        public static string client_secret = "iFy8Q~RJkK.6w~M4eXJ~PFDwr.GnCxVuZgifxc8t";
//    }


//    public static class TestUser
//    {



//        [FunctionName("TestUser")]
//        public static async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
//            ILogger log)
//        {
//            log.LogInformation("C# HTTP trigger Authorize Level Function");



//            var scopes = new[] { "https://graph.microsoft.com/.default" };

//            //var clientSecretCredential = new ClientSecretCredential(AppDefault.tanentId,
//            //    AppDefault.clientId, AppDefault.client_secret);


//            var clientSecretCredential = new ClientSecretCredential(ApiGraphManagement.tanentId,
//                ApiGraphManagement.clientId, ApiGraphManagement.client_secret);


//            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

//            String id = "0b3f250a-31d2-4d41-9f2b-7436c1a6f318";
//            var form = await req.ReadFormAsync();
//            var file = req.Form.Files["file"];

//            using var stream = file.OpenReadStream();
//            var resultIUser = await graphClient.Users[id].Request().Select(d=>new { d.Id }).GetAsync();
//            var result = await graphClient.Users[id].Photo.Content.Request().PutAsync(stream);

//            var profiles = await graphClient.Users[id].Photo.Content.Request().GetAsync();





      



//            //  graphClient.Users["76cbc008-71bb-49fe-bc50-90c1bb167824"].Photo.

         

//            //var userOhoto = await graphClient.Users["76cbc008-71bb-49fe-bc50-90c1bb167824"].
                
               




//            //



//            //String name = "";
//            //if (req.HttpContext.User.Identity.IsAuthenticated)
//            //{
//            //    name = "Authorized with id=" + req.HttpContext.User.GetUserId();
//            //}
//            //else
//            //    name = "UnAuthorized";




//            return new OkObjectResult("");
//        }
//    }
//}
