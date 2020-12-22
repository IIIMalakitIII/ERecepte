using System.Collections.Generic;

namespace EReceipt.DAL.Entities
{
    public class MedicalInstitution
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
