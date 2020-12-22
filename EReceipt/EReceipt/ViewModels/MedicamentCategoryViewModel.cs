using System.Collections.Generic;

namespace EReceipt.ViewModels
{
    public class MedicamentCategoryViewModel
    {
        public int Id { get; set; }

        public  string Name { get; set; }

        public ICollection<SelectViewModel> Medicaments { get; set; }
    }
}
