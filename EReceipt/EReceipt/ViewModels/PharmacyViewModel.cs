using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EReceipt.ViewModels
{
    public class PharmacyViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        public virtual ICollection<SelectViewModel> Medicaments { get; set; }
    }
}
