using System;
using System.ComponentModel.DataAnnotations;
using EReceipt.DAL.Enums;

namespace EReceipt.ViewModels
{
    public class RecordViewModel
    {
        public int? Id { get; set; } 

        public DateTime? RecordingTime { get; set; }

        [Required]
        public string Description { get; set; }

        public string RecordStatus { get; set; }

        [Required]
        public int DoctorId { get; set; }

        public int? PatientId { get; set; }

        public string MedicalInstitution { get; set; }

        public SelectViewModel Doctor { get; set; }

        public SelectViewModel Patient { get; set; }
    }
}
