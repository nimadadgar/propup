﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{
    public class WorkOrderType
    {

        [JsonProperty(PropertyName = "id")]
        public Guid Id { set; get; }

        [JsonProperty(PropertyName = "workOrderTypeName")]
        public string WorkOrderTypeName { set; get; }

    }
}
