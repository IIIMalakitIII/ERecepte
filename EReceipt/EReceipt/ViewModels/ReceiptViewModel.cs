using System;
using System.ComponentModel.DataAnnotations;
using EReceipt.DAL.Enums;

namespace EReceipt.ViewModels
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }

        public int? DoctorId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int MedicamentId { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        public string MedicalInstitution { get; set; }

        public string ReceiptStatus { get; set; }

        public SelectViewModel Doctor { get; set; }

        public SelectViewModel Patient { get; set; }

        public MedicamentViewModel Medicament { get; set; }

        public SelectViewModel MedicamentCategory { get; set; }
    }
}
