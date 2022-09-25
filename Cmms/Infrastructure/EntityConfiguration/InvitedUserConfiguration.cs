using Cmms.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InvitedUserConfiguration : IEntityTypeConfiguration<InvitedUser>
{
    public InvitedUserConfiguration()
    {
      
    }

    public void Configure(EntityTypeBuilder<InvitedUser> builder)
    {
        builder.HasNoDiscriminator();
        builder.ToContainer("invitedUser");
        builder.HasPartitionKey(p => p.Id);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ToJsonProperty("id");
        builder.Property(p => p.Email).ToJsonProperty("email");
        builder.Property(p => p.SurName).ToJsonProperty("surName");
        builder.Property(p => p.FirstName).ToJsonProperty("firstName");
        builder.Property(p => p.MobileNumber).ToJsonProperty("mobileNumber");
        builder.Property(p => p.JobTitle).ToJsonProperty("jobTitle");
        builder.Property(p => p.AccessLevels).ToJsonProperty("accessLevels");
        builder.Property(p => p.Expire).ToJsonProperty("expire");

        builder.Property(p => p.InvitedStatus).HasConversion(v => v.ToString(), v =>
            (UserStatusType)Enum.Parse(typeof(UserStatusType), v));


    }
}