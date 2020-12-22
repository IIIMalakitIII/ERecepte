using System.Collections.Generic;
using System.Threading.Tasks;
using EReceipt.DAL.Entities;

namespace EReceipt.BLL.Interface
{
    public interface IMedicamentCategoryService
    {
        Task<MedicamentCategory> GetMedicamentCategoryByMedicamentId(int id);

        Task<List<MedicamentCategory>> GetAll();

        Task CreateRange(List<string> model);

        Task<MedicamentCategory> GetById(int id);

        Task<int> Create(MedicamentCategory model);

        Task Update(MedicamentCategory model);
    }
}
