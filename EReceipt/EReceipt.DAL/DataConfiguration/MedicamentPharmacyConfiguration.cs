using EReceipt.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EReceipt.DAL.DataConfiguration
{
    public class MedicamentPharmacyConfiguration : IEntityTypeConfiguration<MedicamentPharmacy>
    {
        public void Configure(EntityTypeBuilder<MedicamentPharmacy> builder)
        {
            builder.HasKey(sc => new { sc.MedicamentId, sc.PharmacyId });

            builder.HasOne<Medicament>(sc => sc.Medicament)
                .WithMany(s => s.Pharmacies)
                .HasForeignKey(sc => sc.MedicamentId);


            builder.HasOne<Pharmacy>(sc => sc.Pharmacy)
                .WithMany(s => s.Medicaments)
                .HasForeignKey(sc => sc.PharmacyId);

        }
    }
}
