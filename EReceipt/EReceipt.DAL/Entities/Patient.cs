using System;
using System.Collections.Generic;

namespace EReceipt.DAL.Entities
{
    public class Patient
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string Passport { get; set; }

        public string Address { get; set; }

        public Guid CardNumber { get; set; }

        public int? DiseaseHistoryId { get; set; }

        public string Phone { get; set; }

        public ICollection<Record> Records { get; set; }

        public ICollection<Receipt> Receipts { get; set; }

        public ICollection<Confidant> Confidants { get; set; }

        public User User { get; set; }

        public DiseaseHistory DiseaseHistory { get; set; }
    }
}
