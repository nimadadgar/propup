using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{
    public class RequestSparePartStatus
    {
       public Guid Id { set; get; }
        public string RequestSparePartStatusName { set; get; }
        public string Description { set; get; }
    }
}
