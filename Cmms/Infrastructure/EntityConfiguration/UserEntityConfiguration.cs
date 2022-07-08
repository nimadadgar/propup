using Cmms.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public UserEntityConfiguration()
    {
      
    }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasNoDiscriminator();
        builder.ToContainer("user");
        builder.Property(p => p.Id).ToJsonProperty("id");
        builder.Property(p => p.Email).ToJsonProperty("email");
        builder.Property(p => p.Role).ToJsonProperty("role");
        builder.Property(p => p.Company).ToJsonProperty("company"); 
        builder.Property(p => p.FullName).ToJsonProperty("fullName");
     
        builder.HasPartitionKey(p => p.Id);
        builder.HasKey(p => p.Id);




    }
}