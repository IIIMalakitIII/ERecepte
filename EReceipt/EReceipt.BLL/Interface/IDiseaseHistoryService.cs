using EReceipt.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EReceipt.BLL.Interface
{
    public interface IDiseaseHistoryService
    {
        Task<DiseaseHistory> GetDiseaseHistoryByPatientId(int id);

        Task<List<DiseaseHistory>> GetAll();

        Task<DiseaseHistory> GetById(int id);

        Task<int> Create(DiseaseHistory model);

        Task Update(DiseaseHistory model);
    }
}
