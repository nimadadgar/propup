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
    public interface IUserService : IRepository<User>
    {
        
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        User Add(User user);

    }
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public UserService(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));


        }
        public User Add(User user)
        {
           return _context.Users.Add(user).Entity;

        }


        public Task<User> GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefaultAsync(d => d.Email == email);
        }

        public Task<User> GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
