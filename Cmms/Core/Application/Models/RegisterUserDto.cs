using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Application.Models
{
    public class RegisterUserDto
    {
        public string fullName { set; get; }
        public string email { set; get; }
        public string password { set; get; }
        public string company { set; get; }


    }
}
