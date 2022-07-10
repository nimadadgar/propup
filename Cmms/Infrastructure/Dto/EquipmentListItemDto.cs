using Cmms.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public record EquipmentListItemDto
    {
        public EquipmentListItemDto(Guid id, string equipmentName, string description, EquipmentStatusType status)
        {
            this.id = id;
            this.equipmentName = equipmentName;
            this.description = description;
            this.status = status;
        }


         public Guid id { get; init; }
        public string equipmentName { get; init; }

        public string description { get; init; }

        public EquipmentStatusType status { get; init; }

        public static EquipmentListItemDto ToItem(Equipment eq)
        {
            return new EquipmentListItemDto(eq.Id, eq.EquipmentName, eq.Description, eq.CurrentStatus);
        }
 
    }
}
