using Cmms.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public record UpdateEquipmentDto
    {



        [Required(ErrorMessage = "please enter your id ")]
        public Guid id { init; get; }

        [Required(ErrorMessage ="please enter your equipmentName")]
        public string equipmentName { init; get; }

        [Required(ErrorMessage = "please enter your description")]
        public string description { init; get; }

        public EquipmentStatusType status { init; get; }
 
    }
}
