using EReceipt.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EReceipt.DAL.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Confidant> Confidants { get; set; }

        public DbSet<DiseaseHistory> DiseaseHistories { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<MedicalInstitution> MedicalInstitutions { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<MedicamentCategory> MedicamentCategories { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Pharmacy> Pharmacies { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<Record> Records { get; set; }

        public DbSet<MedicamentPharmacy> MedicamentPharmacy { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
