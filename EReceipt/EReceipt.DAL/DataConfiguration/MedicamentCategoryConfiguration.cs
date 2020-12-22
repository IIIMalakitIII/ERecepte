using EReceipt.DAL.Entities;
using EReceipt.DAL.Сonstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EReceipt.DAL.DataConfiguration
{
    public class MedicamentCategoryConfiguration : IEntityTypeConfiguration<MedicamentCategory>
    {
        public void Configure(EntityTypeBuilder<MedicamentCategory> builder)
        {
            builder.ToTable(TableName.MedicamentCategories);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);
        }
    }
}
