using Cmms.Infrastructure.Context;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{
    public class Equipment: EntityBase
    {
        public Equipment()
        {
           SpareParts=new HashSet<SparePart>();
        }

        public Guid Id { set; get; }
        public string EquipmentName { set; get; }
        public string Description { set; get; }

     public  ICollection<SparePart> SpareParts { set; get; }



        //private List<SparePart> _sparePart = new List<SparePart>();
        //public IReadOnlyCollection<SparePart> SpareParts => _sparePart;





        private List<WorkOrderHistory> _workorderHistory = new List<WorkOrderHistory>();


        public IReadOnlyCollection<WorkOrderHistory> WorkOrderHistory => _workorderHistory;

        public EquipmentStatusType CurrentStatus { set; get; }
      

        public SparePart AddSparePart(SparePart part)
        {
            SpareParts.Add(part);
           // _sparePart.Add(part);
            return part;
        }

    }

   
}
