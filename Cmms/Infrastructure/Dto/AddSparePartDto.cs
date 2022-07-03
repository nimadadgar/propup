using Cmms.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public class AddSparePartDto
    {

        public Guid? id { set; get; }

        [Required(ErrorMessage = "please enter your equipment id")]
        public Guid equipmentId { set; get; }


        [Required(ErrorMessage ="please enter your equipmentName")]
        public string partNumber { set; get; }


        [Required(ErrorMessage = "please enter number of use spare part")]
        public int useCount { set; get; }


        [Required(ErrorMessage = "please enter your description")]
        public string description { set; get; }
 
    }
}
