using System.Collections.Generic;
using System.Threading.Tasks;
using EReceipt.DAL.Entities;

namespace EReceipt.BLL.Interface
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetDoctorByInstituationId(int id);

        Task<List<Doctor>> GetAll();

        Task<Doctor> GetById(int id);

        Task Update(Doctor model);
        
    }
}
