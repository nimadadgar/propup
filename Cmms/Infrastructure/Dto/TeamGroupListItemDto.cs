using Cmms.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public class TeamGroupListItemDto
    {
        
        public TeamGroupListItemDto(Guid id,string teamName,string description,int memberCount)
        {
            this.id = id;
            this.teamName = teamName;
            this.description = description;
            this.memberCount = memberCount;
        }
        public Guid id {private set; get; }
        public string teamName { private set; get; }
        public string description { private set; get; }

        public int memberCount { private set; get; }

        public static TeamGroupListItemDto ToListItem(TeamGroup x)
        {
      return new TeamGroupListItemDto(x.Id, x.TeamGroupName, x.Description,  x.Members.Count() );
            
        }


    }
}
