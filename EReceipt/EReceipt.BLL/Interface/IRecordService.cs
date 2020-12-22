using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EReceipt.DAL.Entities;
using EReceipt.DAL.Enums;

namespace EReceipt.BLL.Interface
{
    public interface IRecordService
    {
        Task<Record> GetRecordByIdAsync(int id);
        Task<List<Record>> GetPatientRecordsAsync(string userId);

        Task<List<Record>> GetPatientRecordsAsync(int id);

        Task<List<Record>> GetDoctorRecordsAsync(string userId);

        Task<Record> CreateRecordAsync(Record model, string userId);

        Task<Record> UpdateRecord(Record model, string userId);

        Task<Record> UpdateRecordStatusLikeDoctor(RecordStatus status, int recordId, string userId);

        Task<Record> UpdateRecordStatusLikePatient(RecordStatus status, int recordId, string userId);

    }
}
