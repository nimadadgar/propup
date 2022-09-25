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
using System.Net;

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

        private const string Meta = nameof(Meta);
        public const string PartitionKey = nameof(PartitionKey);


        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<InvitedUser> InvitedUsers  { get; set; }
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Factory> Factories { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<TeamGroup>  TeamGroups{ get; set; }

        public  string ComputePartitionKey<T>()
        where T : class => typeof(T).Name;


        public void SetPartitionKey<T>(T entity)
           where T : class =>
               Entry(entity).Property(PartitionKey).CurrentValue =
                   ComputePartitionKey<T>();

        public async ValueTask<T> FindMetaAsync<T>(string key)
         where T : class
        {
            var partitionKey = ComputePartitionKey<T>();
            try
            {
                return await FindAsync<T>(key, partitionKey);
            }
            catch (Microsoft.Azure.Cosmos.CosmosException ce)
            {
                if (ce.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration<Equipment>(new EquipmentEntityConfiguration());
            modelBuilder.ApplyConfiguration<TeamGroup>(new TeamGroupEntityConfiguration());
            modelBuilder.ApplyConfiguration<User>(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration<Audit>(new AuditLogEntityConfiguration());
            modelBuilder.ApplyConfiguration<InvitedUser>(new InvitedUserConfiguration());
            modelBuilder.Entity<WorkOrder>(m =>
            {
                m.ToContainer("workOrder");
                m.HasNoDiscriminator();
                m.HasPartitionKey(d => d.EquipmentId);
                m.HasKey(d => d.WorkOrderNumber);
            });


            string PartitionKey = nameof(PartitionKey);

            var jobModel = modelBuilder.Entity<Job>();
            jobModel.Property<string>(PartitionKey);
            jobModel.HasPartitionKey(PartitionKey);
            jobModel.ToContainer("Meta")
                .HasKey(nameof(Job.JobTitle), PartitionKey);
            jobModel.Property(t => t.ETag)
                .IsETagConcurrency();


            var access = modelBuilder.Entity<AccessLevel>();
            access.Property<string>(PartitionKey);
            access.HasPartitionKey(PartitionKey);
            access.ToContainer(Meta)
                .HasKey(nameof(AccessLevel.AccessLevelName), PartitionKey);
            access.Property(t => t.ETag)
                .IsETagConcurrency();


            var factory = modelBuilder.Entity<Factory>();
            factory.Property<string>(PartitionKey);
            factory.HasPartitionKey(PartitionKey);
            factory.ToContainer("Meta")
                .HasKey(nameof(Factory.FactoryName), PartitionKey);
            factory.Property(t => t.ETag)
                .IsETagConcurrency();



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

            var _username = "unknow";// _claimsPrincipalAccessor.Principal.Email();

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
