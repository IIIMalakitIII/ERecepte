using System.Collections.Generic;
using System.Threading.Tasks;
using EReceipt.DAL.Entities;

namespace EReceipt.BLL.Interface
{
    public interface IPharmacyService
    {
        Task<Pharmacy> GetPharmacyByMedicamentId(int id);

        Task<List<Pharmacy>> GetAll();

        Task<Pharmacy> GetById(int id);

        Task Update(Pharmacy model);
    }
}
