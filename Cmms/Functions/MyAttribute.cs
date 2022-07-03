using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Functions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ClaimAttribute : Attribute
    {
        public string _claimType { set; get; }
        public string _claim { get; set; }
        public ClaimAttribute(string claimType, string claimValue)
        {
            _claimType = claimType;
            _claim = claimValue;

        }

        public ClaimAttribute()
        {
            _claimType = string.Empty;
            _claim = string.Empty;

        }
    }



}
