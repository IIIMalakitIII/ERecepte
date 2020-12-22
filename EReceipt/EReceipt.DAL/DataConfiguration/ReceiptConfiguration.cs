using EReceipt.DAL.Entities;
using EReceipt.DAL.Сonstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EReceipt.DAL.DataConfiguration
{
    public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> builder)
        {
            builder.ToTable(TableName.Receipts);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DoctorId)
                .IsRequired();

            builder.Property(x => x.PatientId)
                .IsRequired();

            builder.Property(x => x.MedicamentId)
                .IsRequired();

            builder.Property(x => x.DateStart)
                .IsRequired();

            builder.Property(x => x.DateEnd)
                .IsRequired();

            builder.Property(x => x.ReceiptStatus)
                .IsRequired();

            builder.HasIndex(x => x.DoctorId);
            builder.HasIndex(x => x.MedicamentId);
            builder.HasIndex(x => x.PatientId);

            builder.HasOne(x => x.Medicament)
                .WithMany(x => x.Receipts)
                .HasForeignKey(x => x.MedicamentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Receipts)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Doctor)
                .WithMany(x => x.Receipts)
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
