using System.Collections.Generic;
using System.Threading.Tasks;
using EReceipt.DAL.Entities;

namespace EReceipt.BLL.Interface
{
    public interface IManufacturerService
    {
        Task<Manufacturer> GetManufacturerByMedicamentId(int id);

        Task<List<Manufacturer>> GetAll();

        Task<Manufacturer> GetById(int id);

        Task<int> Create(Manufacturer model);

        Task Update(Manufacturer model);
    }
}
