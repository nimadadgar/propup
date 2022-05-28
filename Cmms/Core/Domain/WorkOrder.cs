using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{
    public class WorkOrder
    {
        public Guid Id { get; set; }
        public string WorkOrderNumber { set; get; }
        public string UserId { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }

        public string Priority { set; get; }



        public Equipment Equipment { set; get; }



        private List<RequestSparePart> _requestSparePart = new List<RequestSparePart>();
        public IReadOnlyCollection<RequestSparePart> UserInstitutes => _requestSparePart;


        public DateTime CreateDate { set; get; }
        public DateTime DueDate { set; get; }
        public WorkOrderType WorkOrderType { set; get; }


        


    }
}
