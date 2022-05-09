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

        #region Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                "https://propupcmmsdb.documents.azure.com:443/",
                "R4H5YXygFDnN0iOVQ1F9fEpZ61i0zCNOmta04buHpNAdLD87EPvqPuk7v15taNi7ZgzxSqg3YqP3G4NiAyIPPw==",
                databaseName: "propupcmmsdb");
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Store");
            modelBuilder.Entity<User>()
                .ToContainer("User");
            modelBuilder.Entity<UserLogin>()
                .ToContainer("UserLogin");

            modelBuilder.Entity<User>()
                .HasNoDiscriminator();

            modelBuilder.Entity<User>().HasPartitionKey(d=>d.Id);
            modelBuilder.Entity<UserLogin>().HasPartitionKey(d => d.UserId);


            modelBuilder.Entity<User>()
          .HasKey(d => d.Email);

            modelBuilder.Entity<User>()
          .Property(d => d.Id).ToJsonProperty("id");


        }
    }

}
