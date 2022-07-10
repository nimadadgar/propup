using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public class FilterColumn
    {
        public FilterColumn(string name,string value)
        {
            this.name = name;
            this.value = value;
        }
        public string name {  get; }
        public string value {  get; }
    }
}
