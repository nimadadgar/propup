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
         Task<UserClaimModel> GetUserById(string id);
        Task<List<UserClaimModel>> GetUsersById(List<string> ids);
        Task<List<UserClaimModel>> GetAllUsers();

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


        public async Task<List<UserClaimModel>> GetAllUsers()
        {
            var result = await _client.Users.Request().GetAsync();
            var data = result.AdditionalData;
            return result.Select(u => UserClaimModel.ToUserClaim(u)).ToList();




        }
        public async Task<UserClaimModel> GetUserById(string id)
        {
            var result = await _client.Users[id].Request().GetAsync();
            var data = result.AdditionalData;
            return UserClaimModel.ToUserClaim(result);
        }

        public async Task<List<UserClaimModel>> GetUsersById(List<string> ids)
        {
           var query= string.Join(" or id eq", ids);

            var listUsers = await (_client.Users.Request().Filter(query).Select(d => new
            {
                displayName = d.DisplayName,
                userId = d.Id,
                company = d.CompanyName,
                identities = d.Identities
            }).GetAsync());

            return listUsers.Select(d => UserClaimModel.ToUserClaim(d)).ToList();
        }


    }
}
