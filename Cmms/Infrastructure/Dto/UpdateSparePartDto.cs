using Cmms.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public record UpdateSparePartDto
    {

        public Guid id { set; get; }

        [Required(ErrorMessage = "please enter your equipment id")]
        public Guid equipmentId { init; get; }

        [Required(ErrorMessage ="please enter your equipmentName")]
        public string partNumber { init; get; }


        [Required(ErrorMessage = "please enter number of use spare part")]
        public int useCount { init; get; }


        [Required(ErrorMessage = "please enter your description")]
        public string description { init; get; }
 
    }
}
