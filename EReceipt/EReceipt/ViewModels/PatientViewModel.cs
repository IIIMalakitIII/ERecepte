using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EReceipt.ViewModels
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Passport { get; set; }

        [Required]
        public string Address { get; set; }

        public Guid CardNumber { get; set; }

        public int? DiseaseHistoryId { get; set; }

        [Required]
        public string Phone { get; set; }

        public ICollection<SelectViewModel> Confidants { get; set; }

        public SelectViewModel DiseaseHistory { get; set; }
    }
}
