using Cmms.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EquipmentEntityConfiguration : IEntityTypeConfiguration<Equipment>
{
    public EquipmentEntityConfiguration()
    {
      
    }

    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.HasNoDiscriminator();
        builder.ToContainer("equipment");
        builder.Property(p => p.EquipmentName).ToJsonProperty("equipmentName");
        builder.Property(p => p.Id).ToJsonProperty("id");
        builder.Property(p => p.Description).ToJsonProperty("description");
       // builder.Property(p => p.SpareParts).ToJsonProperty("spareParts");
        builder.Property(p => p.WorkOrderHistory).ToJsonProperty("workOrderHistory"); 
        builder.Property(p => p.CurrentStatus).ToJsonProperty("currentStatus");


        builder.OwnsMany(t => t.SpareParts, sa =>
        {
            sa.ToJsonProperty("spareParts");

            sa.HasKey(p => p.Id);


            sa.Property(d => d.PartName).ToJsonProperty("partName");
            sa.Property(d => d.Description).ToJsonProperty("description");
            sa.Property(d => d.PartName).ToJsonProperty("partName");
            sa.Property(d => d.Id).ToJsonProperty("id"); 
            sa.Property(d => d.UseCount).ToJsonProperty("useCount"); 
        });






        builder.Ignore(d => d.WorkOrderHistory);


        builder.HasPartitionKey(p => p.Id);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CurrentStatus).HasConversion(v => v.ToString(), v =>
             (EquipmentStatusType)Enum.Parse(typeof(EquipmentStatusType), v));
       


    }
}