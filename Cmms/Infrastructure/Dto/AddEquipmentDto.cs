using Cmms.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public class AddEquipmentDto
    {

        public Guid? id { set; get; }

        [Required(ErrorMessage ="please enter your equipmentName")]
        public string equipmentName { set; get; }


        [Required(ErrorMessage = "please enter your description")]
        public string description { set; get; }

        public EquipmentStatusType status { set; get; }

 
    }
}
