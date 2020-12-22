using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EReceipt.DAL.Entities;

namespace EReceipt.BLL.Interface
{
    public interface IPatientService
    {
        Task<Patient> GetPatientByCode(Guid code);

        Task<List<Patient>> GetAll();

        Task<Patient> GetById(int id);

        Task Update(Patient model);

        Task UpdatePatientCode(int id, Guid code);
    }
}
