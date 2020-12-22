using EReceipt.DAL.Entities;
using EReceipt.DAL.Сonstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EReceipt.DAL.DataConfiguration
{
    public class DiseaseHistoryConfiguration : IEntityTypeConfiguration<DiseaseHistory>
    {
        public void Configure(EntityTypeBuilder<DiseaseHistory> builder)
        {
            builder.ToTable(TableName.DiseaseHistories);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.PatientId)
                .IsRequired();

            builder.HasIndex(x => x.PatientId);

            builder.HasOne(x => x.Patient)
                .WithOne(x => x.DiseaseHistory)
                .HasForeignKey<DiseaseHistory>(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
