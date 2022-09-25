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
    public abstract class EntityBase : IAuditable
    {
        public virtual DateTimeOffset CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTimeOffset UpdatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
    }


}
