using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Application.Exceptions
{
    [Serializable]
    public class AuthorizeException : Exception
    {

        public AuthorizeException(string message)
             : base(message)
        {
        }


    }
}
