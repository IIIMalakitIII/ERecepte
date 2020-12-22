using EReceipt.DAL.Entities;
using EReceipt.DAL.Сonstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EReceipt.DAL.DataConfiguration
{
    public class MedicalInstitutionConfiguration : IEntityTypeConfiguration<MedicalInstitution>
    {
        public void Configure(EntityTypeBuilder<MedicalInstitution> builder)
        {
            builder.ToTable(TableName.MedicalInstitutions);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);
        }

    }
}
