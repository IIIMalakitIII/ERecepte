using System.Collections.Generic;

namespace EReceipt.DAL.Entities
{
    public class Pharmacy
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public User User { get; set; }

        public virtual ICollection<MedicamentPharmacy> Medicaments { get; set; }
    }
}
