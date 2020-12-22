using System;
using EReceipt.DAL.Enums;

namespace EReceipt.DAL.Entities
{
    public class Receipt
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public int MedicamentId { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public ReceiptStatus ReceiptStatus { get; set; }

        public Doctor Doctor { get; set; }

        public Patient Patient { get; set; }

        public Medicament Medicament { get; set; }
    }
}
