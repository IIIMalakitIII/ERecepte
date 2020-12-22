using System.Collections.Generic;

namespace EReceipt.DAL.Entities
{
    public class MedicamentCategory
    {
        public int Id { get; set; }

        public  string Name { get; set; }

        public ICollection<Medicament> Medicaments { get; set; }
    }
}
