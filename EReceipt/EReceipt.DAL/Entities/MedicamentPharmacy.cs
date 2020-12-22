namespace EReceipt.DAL.Entities
{
    public class MedicamentPharmacy
    {
        public int MedicamentId { get; set; }

        public int PharmacyId { get; set; }


        public Medicament Medicament { get; set; }

        public Pharmacy Pharmacy { get; set; }

    }
}
