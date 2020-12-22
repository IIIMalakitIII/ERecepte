using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EReceipt.DAL.Entities;
using EReceipt.DAL.Enums;

namespace EReceipt.BLL.Interface
{
    public interface IReceiptService
    {
        Task<List<Receipt>> GetUserReceipts(string userId, string role);

        Task<List<Receipt>> GetReceiptsByPatientsId(int id);

        Task<List<Receipt>> GetReceiptsByDoctorId(int id);

        Task<List<Receipt>> GetReceiptByPatientActiveId(int id);

        Task<List<Receipt>> GetAll();

        Task<Receipt> GetById(int id);

        Task<int> Create(Receipt model, string userId);

        Task Update(Receipt model);

        Task UpdateReceiptStatus(int id, ReceiptStatus status);
    }
}
