using System.Security.Claims;

namespace Cmms.Models
{
    public class AzureAdToken
    {
        public ClaimsPrincipal User { get; set; }

    }

    internal class TokenConfig
    {
        public string Scopes { get; set; }
        public string Roles { get; set; }
        public string Audience { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
    }
}