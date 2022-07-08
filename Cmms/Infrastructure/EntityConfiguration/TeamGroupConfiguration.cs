using Cmms.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

public class TeamGroupEntityConfiguration : IEntityTypeConfiguration<TeamGroup>
{
    public TeamGroupEntityConfiguration()
    {
      
    }

    public void Configure(EntityTypeBuilder<TeamGroup> builder)
    {
        builder.HasNoDiscriminator();
        builder.ToContainer("teamGroup");
        builder.HasPartitionKey(p => p.Id);
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ToJsonProperty("id");
        builder.Property(p => p.Description).ToJsonProperty("description");
        builder.Property(p => p.TeamGroupName).ToJsonProperty("teamGroupName");

        builder.Property(p => p.Members).ToJsonProperty("members").HasConversion(v => JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<List<Guid>>(v));


     

    }
}