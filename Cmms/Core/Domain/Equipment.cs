using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{
    public class Equipment
    {
        public Guid Id { set; get; }
        public string EquipmentName { set; get; }
        public string Description { set; get; }
        private List<SparePart> _spareParts = new List<SparePart>();
        public IReadOnlyCollection<SparePart> SpareParts => _spareParts;
        private List<WorkOrderHistory> _workorderHistory = new List<WorkOrderHistory>();
        public IReadOnlyCollection<WorkOrderHistory> WorkOrderHistory => _workorderHistory;

        public EquipmentStatusType CurrentStatus { set; get; }


        public SparePart AddSparePart(SparePart part)
        {
            _spareParts.Add(part);
            return part;
        }

    }

   
}
