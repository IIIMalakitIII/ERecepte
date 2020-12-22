using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EReceipt.BLL.Interface;
using EReceipt.Common.Exceptions;
using EReceipt.DAL.Context;
using EReceipt.DAL.Entities;
using EReceipt.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace EReceipt.BLL.Services
{
    public class RecordService: IRecordService
    {
        private readonly AppDbContext _dbContext;

        public RecordService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Record> GetRecordByIdAsync(int id)
        {
            return await _dbContext.Records
                .AsNoTracking()
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .ThenInclude(o => o.MedicalInstitution)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Record>> GetPatientRecordsAsync(int id)
        {
            return await _dbContext.Records
                .AsNoTracking()
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .ThenInclude(o => o.MedicalInstitution)
                .Where(x => x.PatientId == id)
                .OrderByDescending(x => x.RecordingTime)
                .ToListAsync();
        }

        public async Task<List<Record>> GetPatientRecordsAsync(string userId)
        {
            return await _dbContext.Records
                .AsNoTracking()
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                    .ThenInclude(o => o.MedicalInstitution)
                .Where(x => x.Patient.UserId == userId)
                .OrderByDescending(x => x.RecordingTime)
                .ToListAsync();
        }

        public async Task<List<Record>> GetDoctorRecordsAsync(string userId)
        {
            return await _dbContext.Records
                .AsNoTracking()
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .ThenInclude(o => o.MedicalInstitution)
                .Where(x => x.Doctor.UserId == userId)
                .OrderByDescending(x => x.RecordingTime)
                .ToListAsync();
        }

        public async Task<Record> CreateRecordAsync(Record model, string userId)
        {

            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.UserId == userId);

            if (patient == null)
            {
                throw new NullReferenceException("Patient reference not found");
            }

            model.RecordStatus = RecordStatus.InProgress;
            model.PatientId = patient.Id;
            model.RecordingTime = DateTime.Now;

            await _dbContext.Records.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<Record> UpdateRecord(Record model, string userId)
        {
            var record = await _dbContext.Records
                .Include(x => x.Patient)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (record == null)
            {
                throw new NullReferenceException("Record not found");
            }

            if (record.Patient.UserId != userId)
            {
                throw new BusinessLogicException($"You cannot change someone else's record");
            }

            if (record.RecordStatus != RecordStatus.InProgress)
            {
                throw new BusinessLogicException($"You cannot change this entry due to status restrictions");
            }

            record.DoctorId = model.DoctorId;
            record.Description = model.Description;

            _dbContext.Records.Update(record);

            await _dbContext.SaveChangesAsync();

            return record;
        }

        public async Task<Record> UpdateRecordStatusLikeDoctor(RecordStatus status, int recordId, string userId)
        {
            var record = await _dbContext.Records
                .Include(x => x.Doctor)
                .FirstOrDefaultAsync(x => x.Id == recordId);

            if (record == null)
            {
                throw new NullReferenceException("Record not found");
            }

            if (record.Doctor.UserId != userId)
            {
                throw new BusinessLogicException($"You cannot change someone else's record");
            }

            record.RecordStatus = status;

            _dbContext.Records.Update(record);

            await _dbContext.SaveChangesAsync();

            return record;
        }

        public async Task<Record> UpdateRecordStatusLikePatient(RecordStatus status, int recordId, string userId)
        {
            var record = await _dbContext.Records
                .Include(x => x.Patient)
                .FirstOrDefaultAsync(x => x.Id == recordId);

            if (record == null)
            {
                throw new NullReferenceException("Record not found");
            }

            if (record.Patient.UserId != userId)
            {
                throw new BusinessLogicException($"You cannot change someone else's record");
            }

            if (status != RecordStatus.RejectedByPatient)
            {
                throw new BusinessLogicException($"You cannot choose another status");
            }

            record.RecordStatus = status;

            _dbContext.Records.Update(record);

            await _dbContext.SaveChangesAsync();

            return record;
        }
    }
}
