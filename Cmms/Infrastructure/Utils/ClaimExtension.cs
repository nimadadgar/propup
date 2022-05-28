using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Cmms.Infrastructure.Model;

namespace Cmms.Infrastructure.Utils
{
        public static class ClaimsPrincipalExtensions
        {
            public static UserClaimModel UserInfo(this ClaimsPrincipal principal)
            {
                if (principal == null)
            {
                throw new Exception("user is not authorizing");
            }

            UserClaimModel model = new UserClaimModel();
            
            model.userId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.displayName = principal.FindFirst(ClaimTypes.GivenName).Value;

            return model;
            }
        }
    
}
