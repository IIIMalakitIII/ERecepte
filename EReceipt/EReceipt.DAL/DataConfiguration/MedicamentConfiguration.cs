using EReceipt.DAL.Entities;
using EReceipt.DAL.Сonstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EReceipt.DAL.DataConfiguration
{
    public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.ToTable(TableName.Medicaments);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.CategoryId)
                .IsRequired();

            builder.Property(x => x.ManufacturerId)
                .IsRequired();

            builder.Property(x => x.ContentType)
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Instruction)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.HighLength);

            builder.Property(x => x.Description)
                .HasMaxLength(StringLengthConstants.HighLength);

            builder.HasIndex(x => x.CategoryId);
            builder.HasIndex(x => x.ManufacturerId);

            builder.HasOne(x => x.MedicamentCategory)
                .WithMany(x => x.Medicaments)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Manufacturer)
                .WithMany(x => x.Medicaments)
                .HasForeignKey(x => x.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
