using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { set; get; }
        public string Password { set; get; }

        public string Email { set; get; }
        public string Company { set; get; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessDateTime { set; get; }
        public bool IsActive { set; get; }
        public Guid UserIdentity { set; get; }

    }
}
