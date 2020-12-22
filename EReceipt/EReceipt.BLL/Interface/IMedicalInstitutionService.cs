using EReceipt.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EReceipt.BLL.Interface
{
    public interface IMedicalInstitutionService
    {
        Task<MedicalInstitution> GetInstitutionByDoctorId(int id);

        Task<List<MedicalInstitution>> GetAll();

        Task<MedicalInstitution> GetById(int id);

        Task<int> Create(MedicalInstitution model);

        Task Update(MedicalInstitution model);
    }
}
