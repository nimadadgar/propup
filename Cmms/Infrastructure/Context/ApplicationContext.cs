using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cmms.Core.Domain;
using Cmms.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Cmms.Core
{

    public class ApplicationContext : DbContext, IUnitOfWork
    {


        public ApplicationContext(DbContextOptions options)
: base(options)
        {

        }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TeamGroup>  TeamGroups{ get; set; }

        //    public DbSet<WorkOrder> WorkOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // modelBuilder.HasDefaultContainer("equipment");


            modelBuilder.Entity<Equipment>(m =>
            {
                //                m.HasNoDiscriminator();

                m.ToContainer("equipment");
                m.HasPartitionKey(p => p.Id);
                m.HasKey(p => p.Id);
                m.Property(p => p.CurrentStatus).HasConversion(v => v.ToString(), v =>
                     (EquipmentStatusType)Enum.Parse(typeof(EquipmentStatusType), v));
            });


            modelBuilder.Entity<TeamGroup>(m =>
            {
                m.HasNoDiscriminator();
                m.ToContainer("teamGroup");
                m.HasPartitionKey(p => p.Id);
                m.HasKey(p => p.Id);
                m.Property(p => p.Members).HasConversion(v => JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<List<Guid>>(v));

            });


            modelBuilder.Entity<WorkOrder>(m =>
            {
                m.ToContainer("workOrder");
                m.HasPartitionKey(d => d.EquipmentId);
                m.HasKey(d => d.WorkOrderNumber);

            });

            modelBuilder.Entity<User>(m =>
            {
                m.ToContainer("user");
                m.HasPartitionKey(d => d.UserId);
                m.HasKey(d => d.UserId);

            });




        }
    }

}
