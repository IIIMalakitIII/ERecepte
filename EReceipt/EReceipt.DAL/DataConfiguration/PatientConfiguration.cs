using EReceipt.DAL.Entities;
using EReceipt.DAL.Сonstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EReceipt.DAL.DataConfiguration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable(TableName.Patients);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Passport)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.CardNumber)
                .IsRequired();

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.DiseaseHistoryId);

            builder.HasOne(x => x.User)
                .WithOne(x => x.Patient)
                .HasForeignKey<Patient>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DiseaseHistory)
                .WithOne(x => x.Patient)
                .HasForeignKey<Patient>(x => x.DiseaseHistoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
