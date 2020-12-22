using EReceipt.DAL.Entities;
using EReceipt.DAL.Сonstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EReceipt.DAL.DataConfiguration
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.ToTable(TableName.Manufacturers);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.License)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Description)
                .HasMaxLength(StringLengthConstants.HighLength);

        }
    }
}
