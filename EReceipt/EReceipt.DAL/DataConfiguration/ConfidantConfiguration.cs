using EReceipt.DAL.Entities;
using EReceipt.DAL.Сonstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EReceipt.DAL.DataConfiguration
{
    public class ConfidantConfiguration : IEntityTypeConfiguration<Confidant>
    {
        public void Configure(EntityTypeBuilder<Confidant> builder)
        {
            builder.ToTable(TableName.Confidants);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Passport)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.HasIndex(x => x.PatientId);

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Confidants)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
