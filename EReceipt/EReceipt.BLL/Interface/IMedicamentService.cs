using System.Collections.Generic;
using System.Threading.Tasks;
using EReceipt.DAL.Entities;

namespace EReceipt.BLL.Interface
{
    public interface IMedicamentService
    {
        Task<List<Medicament>> GetMedicamentByCategoryId(int id);

        Task<List<Medicament>> GetMedicamentByPharmacyId(int id);

        Task<List<Medicament>> GetMedicamentByManufactorId(int id);

        Task<List<Medicament>> GetAll();

        Task<Medicament> GetById(int id);

        Task<int> Create(Medicament model);

        Task Update(Medicament model);
    }
}
