using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{
    public class AccessLevel
    {
        public AccessLevel()
        {
            Roles = new List<string>();
        }
        public string AccessLevelName { set; get; }
        public string ETag { set; get; }
        public List<string> Roles { set; get; }

    }
}
