using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Context
{


    public interface IAuditable
    {
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
