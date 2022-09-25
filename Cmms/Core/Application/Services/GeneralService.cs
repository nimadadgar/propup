using Cmms.Core.Domain;
using Cmms.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Application
{
  
    public class GeneralService 
    {
        private readonly ApplicationContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public GeneralService(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));


        }
        
        public Task<List<AccessLevel>>  GetAccessLevels()
        {
            return _context.AccessLevels.ToListAsync();
        }
 
        public Task<List<Job>> GetJobs()
        {
            return _context.Jobs.ToListAsync();
        }

        public async Task<Job> GetJobById(string jobTitle)

        {
            string key = _context.ComputePartitionKey<Job>();
            return await _context.Jobs.FindAsync(jobTitle, key);


        }

        public async Task<Factory> GetFactoryById(string factoryName)
        {
            string key = _context.ComputePartitionKey<Factory>();
            return await _context.Factories.FindAsync(factoryName, key);


        }


        public Task<List<Factory>> GetFactories()
        {
                string key = _context.ComputePartitionKey<Factory>();
            return _context.Factories.WithPartitionKey(key).ToListAsync();


        }
    }
}
