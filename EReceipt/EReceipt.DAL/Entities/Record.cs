using System;
using EReceipt.DAL.Enums;

namespace EReceipt.DAL.Entities
{
    public class Record
    {
        public int Id { get; set; } 

        public DateTime RecordingTime { get; set; }

        public RecordStatus RecordStatus { get; set; }

        public string Description { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public Doctor Doctor { get; set; }

        public Patient Patient { get; set; }
    }
}
