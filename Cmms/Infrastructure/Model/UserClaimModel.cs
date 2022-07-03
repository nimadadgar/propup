using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Model
{
    public class UserClaimModel
    {
        public string userId { set; get; }
        public string displayName { set; get; }
        public string email { set; get; }
    }

    public class AzureFunctionSettings
    {
        public string SiteUrl { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
   
        public string CertificateThumbprint { get; set; }
    }
}
