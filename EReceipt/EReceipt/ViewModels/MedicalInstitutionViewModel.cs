using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EReceipt.ViewModels
{
    public class MedicalInstitutionViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        public ICollection<SelectViewModel> Doctors { get; set; }
    }
}
