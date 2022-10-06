using Azure.Core;
using Azure.Identity;
using Cmms.Core.Domain;
using Cmms.Infrastructure.Context;
using Cmms.Infrastructure.Dto;
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
         Task<UserInfoModelDto> GetUserById(string id);
        Task<List<UserInfoModelDto>> GetUsersById(List<string> ids);
        Task<List<UserInfoModelDto>> GetAllUsers();
        Task<UserInfoModelDto> GetUserByEmail(string email);
        Task<string> CreateUser(InvitedUser inviteUser, string password);

    }
    public class GraphService : IGraphService
    {
        private readonly GraphServiceClient _client;
        private readonly string _tenantId;
        private readonly string _clientId;
        private readonly string _b2cExtensionClientApp;

        public GraphService(String tenantId,String clientId,String clientSecret,String b2cExtensionClientApp)
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            _tenantId= tenantId;
            _clientId= clientId;
            _b2cExtensionClientApp = b2cExtensionClientApp;

            var clientSecretCredential = new ClientSecretCredential(tenantId,
            clientId, clientSecret);
            _client = new GraphServiceClient(new TokenCredentialAuthProvider(clientSecretCredential, scopes));
        }

        internal string GetCompleteAttributeName(string attributeName)
            {
            if (string.IsNullOrWhiteSpace(attributeName))
                {
                    throw new System.ArgumentException("Parameter cannot be null", nameof(attributeName));
                }
                return $"extension_{_b2cExtensionClientApp.Replace("-", "")}_{attributeName}";
            }

        internal string AccessLevelsAttribute
        {
            get
            {
                return GetCompleteAttributeName("AccessLevels");
            }
        }
        internal string LocationAttribute
        {
            get
            {
                return GetCompleteAttributeName("Location");
            }
        }
        internal string StatusAttribute
        {
            get
            {
                return GetCompleteAttributeName("Status");
            }
        }
 
        internal IGraphServiceUsersCollectionRequest UserRequest(string filter="")
        {
            var request = _client.Users.Request();
            if (String.IsNullOrEmpty(filter) == false)
                request = request.Filter(filter);

            return UserSelectRequest(request);
        }

        internal IGraphServiceUsersCollectionRequest UserSelectRequest(IGraphServiceUsersCollectionRequest request)
        {
            return request.Select($"id,displayName,identities,mobilePhone,givenName,surName,jobTitle," + $"{LocationAttribute},{AccessLevelsAttribute},{StatusAttribute}");


        }
        
        public async Task<List<UserInfoModelDto>> GetAllUsers()
        {

            var result = await UserRequest().GetAsync();
            return result.Select(u => UserInfoModelDto.ToUserClaim(u,this)).ToList();
        }
        public async Task<UserInfoModelDto> GetUserById(string id)
        {
            var request =await _client.Users[id].Request().Select(e => new
            {
                e.Id,
                e.DisplayName,
                e.GivenName,
                e.Surname,
                e.JobTitle,
                e.AdditionalData,
                e.Identities,
                e.CompanyName
            }).GetAsync();

            return UserInfoModelDto.ToUserClaim(request, this);



        }
        public async Task<UserInfoModelDto> GetUserByEmail(string email)
        {
            var result = await UserRequest($"identities/any(c:c/issuerAssignedId eq '{email}' and c/issuer eq '{_tenantId}')").GetAsync();

            return result.Select(u => UserInfoModelDto.ToUserClaim(u, this)).FirstOrDefault();
        }

        public async Task<List<UserInfoModelDto>> GetUsersById(List<string> ids)
        {
           var query= string.Join(" or id eq", ids);

            var listUsers = await UserRequest(query).GetAsync();

            return listUsers.Select(d => UserInfoModelDto.ToUserClaim(d,this)).ToList();
        }

        public async Task<string> CreateUser(InvitedUser inviteUser,string password)
        {
         
            // Fill custom attributes
            IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
            extensionInstance.Add(AccessLevelsAttribute, string.Join(",", inviteUser.AccessLevels)  );
            extensionInstance.Add(LocationAttribute, inviteUser.LocationName);
            extensionInstance.Add(StatusAttribute, "Available");

            var result = await _client.Users
             .Request()
             .AddAsync(new Microsoft.Graph.User
             {
                 GivenName = inviteUser.FirstName,
                 Surname = inviteUser.SurName,
                 JobTitle = inviteUser.JobTitle,
                 MobilePhone =inviteUser.MobileNumber,
                 DisplayName = $"{inviteUser.FirstName} {inviteUser.SurName}",
                 UserType = "Member",
                 Identities = new List<ObjectIdentity>
                 {
                        new ObjectIdentity()
                        {

                            SignInType = "emailAddress",
                            Issuer = "propupcom.onmicrosoft.com",
                            IssuerAssignedId = inviteUser.Email
                        }
                 },
                 PasswordProfile = new PasswordProfile()
                 {
                     Password = password,
                     ForceChangePasswordNextSignIn = false,
                     ForceChangePasswordNextSignInWithMfa = false,
                 },
                 PasswordPolicies = "DisablePasswordExpiration",
                  AdditionalData = extensionInstance
                });;

            return result.Id;


        }


    }
}
