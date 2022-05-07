using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Context
{
    public interface IRepository<T> where T:class
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
