using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cmms.Core.Domain;
using Cmms.Infrastructure.Context;
using Cmms.Infrastructure.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Cmms.Infrastructure.Utils;
namespace Cmms.Core
{

    public class ApplicationContext : DbContext, IUnitOfWork
    {

        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public ApplicationContext(DbContextOptions options, IClaimsPrincipalAccessor claimsPrincipalAccessor)
: base(options)
        {

            this.ChangeTracker.LazyLoadingEnabled = false;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public ApplicationContext(DbContextOptions options)
: base(options)
        {

            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<Audit> AuditLogs { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<TeamGroup>  TeamGroups{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration<Equipment>(new EquipmentEntityConfiguration());
            modelBuilder.ApplyConfiguration<TeamGroup>(new TeamGroupEntityConfiguration());
            modelBuilder.ApplyConfiguration<User>(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration<Audit>(new AuditLogEntityConfiguration());

            modelBuilder.Entity<WorkOrder>(m =>
            {
                m.ToContainer("workOrder");
                m.HasPartitionKey(d => d.EquipmentId);
                m.HasKey(d => d.WorkOrderNumber);
            });
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            // Get audit entries
            var auditEntries = OnBeforeSaveChanges();

            // Save current entity
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
          var  _username = _claimsPrincipalAccessor.Principal.UserInfo().email;

            ChangeTracker.DetectChanges();
            DateTime dt = DateTime.Now;
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                // Dot not audit entities that are not tracked, not changed, or not of type IAuditable
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || !(entry.Entity is IAuditable))
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.userName = _username;
                auditEntries.Add(auditEntry);

               
                    if (entry.State == EntityState.Added)
                    {
                        ((EntityBase)entry.Entity).CreatedDate = dt;
                        ((EntityBase)entry.Entity).CreatedBy = _username;

                    }

                    ((EntityBase)entry.Entity).UpdatedDate = dt;
                    ((EntityBase)entry.Entity).UpdatedBy = _username;


              


                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = "create";
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = "delete";
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType ="update";
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }


            return auditEntries;

        }

        



    }

}
