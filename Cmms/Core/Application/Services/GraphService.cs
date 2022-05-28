using Azure.Core;
using Azure.Identity;
using Cmms.Core.Domain;
using Cmms.Infrastructure.Context;
using Cmms.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Application.Services
{
    public interface IGraphService 
    {
         Task<UserClaimModel> GetUserById(String id);

    }
    public class GraphService : IGraphService
    {
        private readonly GraphServiceClient _client;
        public GraphService(String tenantId,String clientId,String clientSecret)
        {
                     var scopes = new[] { "https://graph.microsoft.com/.default" };
            var clientSecretCredential = new ClientSecretCredential(tenantId,
                    clientId, clientSecret);
            _client = new GraphServiceClient(new TokenCredentialAuthProvider(clientSecretCredential, scopes));
        }
   

        public async Task<UserClaimModel> GetUserById(String id)
        {
            var result = (await _client.Users[id].Request().Select(d => new 
            {
                displayName = d.DisplayName,
                userId = d.Id,
            }).GetAsync());
            

            
            return new UserClaimModel { displayName=result.DisplayName,userId=result.Id};

        }
    }
}
