using EReceipt.DAL.Entities;
using EReceipt.DAL.Сonstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EReceipt.DAL.DataConfiguration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable(TableName.Doctors);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MedicalInstitutionId);

            builder.Property(x => x.UserId);

            builder.Property(x => x.RecordingAvailable)
                .IsRequired();

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.License)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.MedicalInstitutionId);

            builder.HasOne(x => x.MedicalInstitution)
                .WithMany(x => x.Doctors)
                .HasForeignKey(x => x.MedicalInstitutionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithOne(x => x.Doctor)
                .HasForeignKey<Doctor>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
