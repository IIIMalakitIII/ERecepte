using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EReceipt.BLL.Interface;
using EReceipt.Common.Authentication;
using EReceipt.Common.Exceptions;
using EReceipt.DAL.Context;
using EReceipt.DAL.Entities;
using EReceipt.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace EReceipt.BLL.Services
{
    public class ReceiptService: IReceiptService
    {

        private readonly AppDbContext _dbContext;

        public ReceiptService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Receipt>> GetReceiptsByPatientsId(int id)
        {
            return await _dbContext.Receipts.AsNoTracking()
                .Include(x => x.Doctor)
                    .ThenInclude(o => o.MedicalInstitution)
                .Include(x => x.Medicament)
                    .ThenInclude(o => o.MedicamentCategory)
                .Include(x => x.Patient)
                    .ThenInclude(o => o.Confidants)
                .Where(x => x.PatientId == id)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<Receipt>> GetReceiptsByDoctorId(int id)
        {
            return await _dbContext.Receipts.AsNoTracking()
                .Include(x => x.Doctor)
                .ThenInclude(o => o.MedicalInstitution)
                .Include(x => x.Medicament)
                .ThenInclude(o => o.MedicamentCategory)
                .Include(x => x.Patient)
                .ThenInclude(o => o.Confidants)
                .Where(x => x.DoctorId == id)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

        }

        public async Task<List<Receipt>> GetReceiptByPatientActiveId(int id)
        {
            return await _dbContext.Receipts.AsNoTracking()
                .Include(x => x.Doctor)
                    .ThenInclude(o => o.MedicalInstitution)
                .Include(x => x.Medicament)
                    .ThenInclude(o => o.MedicamentCategory)
                .Include(x => x.Patient)
                    .ThenInclude(o => o.Confidants)
                .Where(x => x.PatientId == id && x.ReceiptStatus == ReceiptStatus.InTheProcess)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<Receipt>> GetAll()
        {
            return await _dbContext.Receipts
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<Receipt>> GetUserReceipts(string userId, string role)
        {
            var query = _dbContext.Receipts
                .AsNoTracking()
                .Include(x => x.Doctor)
                .ThenInclude(o => o.MedicalInstitution)
                .Include(x => x.Medicament)
                    .ThenInclude(o => o.MedicamentCategory)
                .Include(x => x.Medicament.Manufacturer)
                .Include(x => x.Patient)
                .ThenInclude(o => o.Confidants);
            if (role == Role.Doctor)
            {
                return await query
                    .Where( x=> x.Doctor.UserId == userId)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
            }

            return await query
                .Where(x => x.Patient.UserId == userId)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<Receipt> GetById(int id)
        {
            return await _dbContext.Receipts
                .AsNoTracking()
                .Include(x => x.Doctor)
                    .ThenInclude(o => o.MedicalInstitution)
                .Include(x => x.Medicament)
                    .ThenInclude(o => o.MedicamentCategory)
                .Include(x => x.Patient)
                    .ThenInclude(o => o.Confidants)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Create(Receipt model, string userId)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.UserId == userId);

            if (doctor == null)
            {
                throw new NullReferenceException("Doctor reference not found");
            }

            model.DoctorId = doctor.Id;
            model.ReceiptStatus = ReceiptStatus.InTheProcess;

            await _dbContext.Receipts.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task Update(Receipt model)
        {
            if (!_dbContext.Receipts.Any(x => x.Id == model.Id))
            {
                throw new BusinessLogicException($"Receipt with id: {model.Id} doesn't exist");
            }

            _dbContext.Receipts.Update(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateReceiptStatus(int id, ReceiptStatus status)
        {
            var receipt = await _dbContext.Receipts.FirstOrDefaultAsync(x => x.Id == id);
            if (receipt == null)
            {
                throw new BusinessLogicException($"Receipt with id: {id} doesn't exist");
            }

            receipt.ReceiptStatus = status;

            _dbContext.Receipts.Update(receipt);

            await _dbContext.SaveChangesAsync();
        }
    }
}
