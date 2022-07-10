using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public class PaginationFilter
    {
        public int pageNumber { get; }
        public int pageSize { get; }
        public List<FilterColumn> filters {  get; }
    
        public PaginationFilter(int pageNumber, int pageSize,List<FilterColumn> filters)
        {
            this.pageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.pageSize = pageSize > 10 ? 10 : pageSize;
            if (filters== null || filters.Count == 0)
            {
               this. filters = new List<FilterColumn>();
            }
            else
            this.filters = filters;

        }
    }
}
