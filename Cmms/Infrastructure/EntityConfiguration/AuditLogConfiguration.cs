using Cmms.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AuditLogEntityConfiguration : IEntityTypeConfiguration<Audit>
{
    public AuditLogEntityConfiguration()
    {
      
    }

    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.HasNoDiscriminator();
        builder.ToContainer("auditLog");

        builder.Property(p=>p.Id).ToJsonProperty("id");
        builder.Property(p => p.PrimaryKey).ToJsonProperty("primaryKey");
        builder.Property(p => p.UserName).ToJsonProperty("userName");


    }
}