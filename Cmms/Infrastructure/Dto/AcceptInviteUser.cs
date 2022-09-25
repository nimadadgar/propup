using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public record AcceptInviteUserDto
    {
        public Guid Id { get; init; }
        public string Password { set; get; }

    }
}
