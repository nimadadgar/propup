using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cmms.Core.Domain;
using Cmms.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Cmms.Core
{

    public class ApplicationContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }

        public ApplicationContext(DbContextOptions options)
: base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Store");
            modelBuilder.Entity<User>()
                .ToContainer("User");

            modelBuilder.Entity<User>()
                .HasNoDiscriminator();

            //modelBuilder.Entity<User>().HasPartitionKey(d=>d.Id);
            //modelBuilder.Entity<UserLogin>().HasPartitionKey(d => d.UserId);


            modelBuilder.Entity<User>()
          .HasKey(d => d.Email);

            modelBuilder.Entity<User>()
          .Property(d => d.Id).ToJsonProperty("id");


        }
    }

}
