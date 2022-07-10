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
    public class TeamGroupItemDto
    {
        
        public TeamGroupItemDto(Guid id,string teamName,string description,List<TeamGroupMemberItemDto> members)
        {
            this.id = id;
            this.teamName = teamName;
            this.description = description;
            this.members = members;
        }
        public Guid id {private set; get; }
        public string teamName { private set; get; }
        public string description { private set; get; }

        public List<TeamGroupMemberItemDto> members { set; get; }


        public static TeamGroupItemDto ToItem(TeamGroup x)
        {
      return new TeamGroupItemDto(x.Id, x.TeamGroupName, x.Description,  
          x.Members.Select(m=>new TeamGroupMemberItemDto { id = m }).ToList()
          );
            
        }


    }
}
