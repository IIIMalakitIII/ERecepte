using System.Collections.Generic;

namespace EReceipt.DAL.Entities
{
    public class Manufacturer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string License { get; set; }

        public string Description { get; set; }

        public ICollection<Medicament> Medicaments { get; set; }
    }
}
