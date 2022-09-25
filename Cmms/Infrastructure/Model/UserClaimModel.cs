using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Model
{
    public class UserClaimModel
    {
        public UserClaimModel(string userId,string email,string displayName,string company)
        {
            this.userId = userId;
            this.email = email;   
            this.company = company;
        }
        public string userId { init; get; }
        public string company { init; get; }
        public string displayName { init; get; }
        public string email { init; get; }
        public string role { init; get; }

        
        public static UserClaimModel ToUserClaim(Microsoft.Graph.User user)
        {
            return new UserClaimModel(user.Id, user.Mail,user.DisplayName, user.CompanyName);

        }
    }

    public class AzureFunctionSettings
    {
        public string SiteUrl { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
   
        public string CertificateThumbprint { get; set; }
    }
}
