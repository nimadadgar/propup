using Cmms.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public class UpdateTeamGroupDto
    {
        [Required(ErrorMessage = "please enter id of team group ")]
        public Guid id { set; get; }

        [Required(ErrorMessage = "please enter your team name")]
        public string teamName { set; get; }
        public string description { set; get; }

        public List<Guid> users { set; get; }
        


    }
}
