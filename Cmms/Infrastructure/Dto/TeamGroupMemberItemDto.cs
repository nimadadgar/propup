using Cmms.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public class TeamGroupMemberItemDto
    {
        public Guid id { set; get; }
        public string fullName { set; get; }


    }
}
