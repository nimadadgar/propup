using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{
    public class WorkOrder
    {

        public Guid EquipmentId { set; get; }
        public string EquipmentName { set; get; }
        public string WorkOrderNumber { set; get; }

        public User User { set; get; }

        public WorkOrderType WorkOrderType { set; get; }

        public string Title { set; get; }

        public string Description { set; get; }

        public string Priority { set; get; }


        public WorkOrderStatus WorkOrderStatus { set; get; }

       
        private List<RequestSparePart> _requestSparePart = new List<RequestSparePart>();
        public IReadOnlyCollection<RequestSparePart> RequestSparePart => _requestSparePart;


        public DateTime CreateDate { set; get; }


        [JsonProperty(PropertyName = "dueDate")]
        public DateTime DueDate { set; get; }


        [JsonProperty(PropertyName = "finishDate")]
        public DateTime FinishDate { set; get; }

    }
}
