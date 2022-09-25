using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{

    public enum UserStatusType
    {

        [Display(Name = "Pending")]
        Pending = 1,


        [Display(Name = "Accept")]
        Accept = 2,


        [Display(Name = "Reject")]
        Reject = 3,

             [Display(Name = "Expired")]
        Expired = 4
    }
}
