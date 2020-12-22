using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EReceipt.ViewModels
{
    public class ManufacturerViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string License { get; set; }

        public string Description { get; set; }

        public ICollection<SelectViewModel> Medicaments { get; set; }
    }
}
