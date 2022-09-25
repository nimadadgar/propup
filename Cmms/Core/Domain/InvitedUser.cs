using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{
    public class InvitedUser
    {
        public InvitedUser()
        {
            AccessLevels = new List<string>();
        }

        public Guid Id { get; set; }
        public string FirstName { set; get; }
        public string SurName { set; get; }
        public string Email { set; get; }
        public string MobileNumber { set; get; }
        public string JobTitle { set; get; }
        public string LocationName { set; get; }
        public DateTimeOffset Expire { set; get; }
        public string ETag { set; get; }
        public List<string> AccessLevels { set; get; }

        public UserStatusType InvitedStatus { set; get; }
    }
}
