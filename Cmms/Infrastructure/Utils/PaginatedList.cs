using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Utils
{

    public class PaginatedList<T> 
    {
        public int pageIndex { get; private set; }
        public int totalPages { get; private set; }
        public List<T> items { get; private set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.items = new List<T>();
            this.pageIndex = pageIndex;
            this.totalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.items.AddRange(items);
        }

        public bool hasPreviousPage => pageIndex > 1;

        public bool hasNextPage => pageIndex < totalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
