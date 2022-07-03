using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{

    public enum EquipmentStatusType
    {

        [Display(Name = "Normal")]
        Normal = 1,


        [Display(Name = "Slow Run")]
        SlowRun = 2,


        [Display(Name = "Not Working")]
        NotWorking = 3    
    }
}
