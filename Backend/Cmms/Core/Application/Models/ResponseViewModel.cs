using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Application.Models
{
  
    public class ResponseViewModel
    {


        public object data { set; get; }

        public ResponseViewModel(object data)
        {
            this.data = data;

        }

    }
    public class BadResponseViewModel
    {
        public string message { set; get; }
        public string traceId { set; get; }
       public object data { set; get; } 

        public BadResponseViewModel(String message, string traceId)
        {
            this.message = message.ToString();
        }
        public BadResponseViewModel(String message, string traceId,object data)
        {
            this.message = message.ToString();
            this.data = data;
        }
    }
}
