using System.Collections.Generic;
using EReceipt.DAL.Entities;
using EReceipt.DAL.Сonstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EReceipt.DAL.DataConfiguration
{
    public class PharmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {
            builder.ToTable(TableName.Pharmacies);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(StringLengthConstants.SmallLength);

            builder.HasIndex(x => x.UserId);

            builder.HasOne(x => x.User)
                .WithOne(x => x.Pharmacy)
                .HasForeignKey<Pharmacy>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
