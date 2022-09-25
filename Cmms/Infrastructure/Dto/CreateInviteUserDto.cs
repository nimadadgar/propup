using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public record CreateInviteUserDto
    {
        public Guid Id { get; init; }
        public string FirstName { init; get; }
        public string SurName { init; get; }
        public string Email { init; get; }
        public string MobileNumber { init; get; }
        public string JobTitle { init; get; }
        public string FactoryName { init; get; }
        public List<string> AccessLevels { init; get; }

    }
}
