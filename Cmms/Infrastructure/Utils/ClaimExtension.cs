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
            model.email = principal.FindFirst(ClaimTypes.Email).Value;

            return model;
            }

        public static bool IsClaimExist(this ClaimsPrincipal principal,String claimType,String claimValue)
        {
            if (principal == null)
            {
                return false;
            }

            string _existClaim = principal.FindFirst(claimType).Value;
            if (_existClaim == claimValue)
            {
                return true;
            }

            return false;


        }
    }
    
}
