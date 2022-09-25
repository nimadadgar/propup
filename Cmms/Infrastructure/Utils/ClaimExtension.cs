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
           
        public static Guid UserId(this ClaimsPrincipal principal)
        {
           return  Guid.Parse(GetClaim(principal, ClaimTypes.NameIdentifier));
        }

        public static string Email(this ClaimsPrincipal principal)
        {
            return GetClaim(principal, "email");
        }
        public static string Name(this ClaimsPrincipal principal)
        {
            return GetClaim(principal, "name");
        }
        public static string  Company(this ClaimsPrincipal principal)
        {
            return GetClaim(principal, "extension_Department");
        }

        public static string GetClaim(this ClaimsPrincipal principal,String claimType)
        {
            if (principal.FindFirst(claimType) != null)
                return principal.FindFirst(claimType).Value;



            return String.Empty;


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
