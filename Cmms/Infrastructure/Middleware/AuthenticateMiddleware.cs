using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Cmms.Core.Application;
using Cmms.Core.Application.Exceptions;
using Cmms.Core.Application.Services;
using Cmms.Functions;
using Cmms.Infrastructure.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cmms.Infrastructure.Middleware
{
    public interface IClaimsPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; set; }
      
    }
    
    public class ClaimsPrincipalAccessor : IClaimsPrincipalAccessor
    {
        private readonly ContextHolder _context = new();

        public ClaimsPrincipal Principal
        {
            get => _context.Context;
            set
            {
                
            _context.Context =   value ;
            }
        }

        private class ContextHolder
        {
            public ClaimsPrincipal Context;
        }
    }

  
    public class ClientClaimPrincipalItemViewModel
    {
        public string typ { get; set; }
        public string val { get; set; }
    }

    public class ClientClaimPrincipalViewModel
    {
        public string auth_typ { get; set; }
        public List<ClientClaimPrincipalItemViewModel> claims { get; set; }
        public string name_typ { get; set; }
        public string role_typ { get; set; }
    }




    public class AuthenticateMiddleware : IFunctionsWorkerMiddleware
    {

        private readonly IGraphService _graphService;
        private readonly IUserService _userService;

        public AuthenticateMiddleware(IGraphService graphService, IUserService userService)
        {
            _graphService = graphService;
            _userService = userService;
        }
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {

            //(Type featureType, object featureInstance) = context.Features.SingleOrDefault(x => x.Key.Name == "IFunctionBindingsFeature");
            //    var inputData = featureType.GetProperties().SingleOrDefault(p => p.Name == "InputData")?.GetValue(featureInstance) as IReadOnlyDictionary<string, object>;
            //    var requestData = inputData?.Values.SingleOrDefault(obj => obj is HttpRequestData) as HttpRequestData;
            //var accessor = context.InstanceServices.GetRequiredService<IClaimsPrincipalAccessor>();

            //string header = "";
            //if (bool.Parse(Environment.GetEnvironmentVariable("IsDevelopment")) == true)
            //{
            //    header = "eyJhdXRoX3R5cCI6ImFhZCIsImNsYWltcyI6W3sidHlwIjoiaXNzIiwidmFsIjoiaHR0cHM6XC9cL3Byb3B1cGNvbS5iMmNsb2dpbi5jb21cLzY2ZjY1NTdkLTFkODctNGUzZC05NTFlLWNiY2JlMGFjMTFiMVwvdjIuMFwvIn0seyJ0eXAiOiJleHAiLCJ2YWwiOiIxNjYyMDI1OTM1In0seyJ0eXAiOiJuYmYiLCJ2YWwiOiIxNjYyMDIyMzM1In0seyJ0eXAiOiJhdWQiLCJ2YWwiOiIyMzAwNjQwZi02MThiLTQ0MjItYjUzNC1iZTY5NjQzMjA1NzEifSx7InR5cCI6Imh0dHA6XC9cL3NjaGVtYXMubWljcm9zb2Z0LmNvbVwvaWRlbnRpdHlcL2NsYWltc1wvb2JqZWN0aWRlbnRpZmllciIsInZhbCI6IjA4Y2VlNDc2LTg3MGYtNGQ2NS1iMjI3LTFhODk3Yjc4M2Q4MiJ9LHsidHlwIjoiaHR0cDpcL1wvc2NoZW1hcy54bWxzb2FwLm9yZ1wvd3NcLzIwMDVcLzA1XC9pZGVudGl0eVwvY2xhaW1zXC9uYW1laWRlbnRpZmllciIsInZhbCI6IjA4Y2VlNDc2LTg3MGYtNGQ2NS1iMjI3LTFhODk3Yjc4M2Q4MiJ9LHsidHlwIjoibmFtZSIsInZhbCI6IkphdmFkIGRpc3BsYXkifSx7InR5cCI6ImV4dGVuc2lvbl9EZXBhcnRtZW50IiwidmFsIjoiTXljb21wYW55In0seyJ0eXAiOiJlbWFpbHMiLCJ2YWwiOiJqYXZhZDEyM0BnbWFpbC5jb20ifSx7InR5cCI6InRmcCIsInZhbCI6IkIyQ18xX1NpZ25BbmRTaWdudXAifSx7InR5cCI6Im5vbmNlIiwidmFsIjoiOVR3QTRZT1pTWVpMWjlDS0pxSm5HZyJ9LHsidHlwIjoiYXpwIiwidmFsIjoiMjMwMDY0MGYtNjE4Yi00NDIyLWI1MzQtYmU2OTY0MzIwNTcxIn0seyJ0eXAiOiJ2ZXIiLCJ2YWwiOiIxLjAifSx7InR5cCI6ImlhdCIsInZhbCI6IjE2NjIwMjIzMzUifV0sIm5hbWVfdHlwIjoiaHR0cDpcL1wvc2NoZW1hcy54bWxzb2FwLm9yZ1wvd3NcLzIwMDVcLzA1XC9pZGVudGl0eVwvY2xhaW1zXC9uYW1lIiwicm9sZV90eXAiOiJodHRwOlwvXC9zY2hlbWFzLm1pY3Jvc29mdC5jb21cL3dzXC8yMDA4XC8wNlwvaWRlbnRpdHlcL2NsYWltc1wvcm9sZSJ9";
            //}
            //else
            //{
            //    var isHeaderExist = requestData.Headers.TryGetValues("x-ms-client-principal",out  var headers);   
            //    if (!isHeaderExist)
            //        throw new BadRequestException("parameters is wrong");

            //    header = headers.First();

            //}

            //var valueBytes = System.Convert.FromBase64String(header);
            //var decodeClaimsJson= Encoding.ASCII.GetString(valueBytes);
            // var principal = JsonSerializer.Deserialize<ClientClaimPrincipalViewModel>(decodeClaimsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //List<Claim> claims = new List<Claim>();
            //foreach(var p in principal.claims)
            //{
            //    claims.Add(new Claim(p.typ, p.val));
            //};

            //claims.Add(new Claim(ClaimTypes.Role, "user"));
                

            //    ClaimsIdentity clientIdentity = new ClaimsIdentity(claims,"Bearer");
            //    accessor.Principal = new ClaimsPrincipal(clientIdentity);
            await next(context);

        }
    }

}
