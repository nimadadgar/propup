using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Cmms.Infrastructure.Model;

namespace Cmms.Infrastructure.Utils
{


            
    
        public static class StringExtension
    {
            public static string FixNull(this string data)
            {
            if (String.IsNullOrWhiteSpace(data))
                return "";
            else
                return data;
           
            }

    }
    
}
