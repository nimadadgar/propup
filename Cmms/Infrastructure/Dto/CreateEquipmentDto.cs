using Cmms.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public record CreateEquipmentDto
    {

        [Required(ErrorMessage ="please enter your equipmentName")]
        public string equipmentName { get; init; }

        [Required(ErrorMessage = "please enter your description")]
        public string description { init; get; }

        public EquipmentStatusType status { init; get; }
 
    }
}
