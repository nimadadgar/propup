using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{
    public class RequestSparePart
    {
       public Guid Id { set; get; }
       public RequestSparePartStatus RequestSparePartStatus { set; get; }
       public DateTime RequestDate { set; get; }
       public DateTime ChangeStatusDateTime { set; get; }
       public float RequestNumber { set; get; }
       public string Description { set; get; }
    }
}
