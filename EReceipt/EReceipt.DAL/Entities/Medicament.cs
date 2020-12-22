using System.Collections.Generic;

namespace EReceipt.DAL.Entities
{
    public class Medicament
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Picture { get; set; }

        public string ContentType { get; set; }

        public int CategoryId { get; set; }

        public int ManufacturerId { get; set; }

        public string Instruction { get; set; }

        public string Description { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public MedicamentCategory MedicamentCategory { get; set; }

        public ICollection<MedicamentPharmacy> Pharmacies { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}
