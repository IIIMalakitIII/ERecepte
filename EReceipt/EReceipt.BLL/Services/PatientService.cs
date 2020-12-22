using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EReceipt.BLL.Interface;
using EReceipt.Common.Exceptions;
using EReceipt.DAL.Context;
using EReceipt.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EReceipt.BLL.Services
{
    public class PatientService: IPatientService
    {
        private readonly AppDbContext _dbContext;

        public PatientService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Patient> GetPatientByCode(Guid code)
        {
            return await _dbContext.Patients.AsNoTracking()
                .Include(x => x.Receipts)
                .Include(x => x.Records)
                .Include(x => x.Confidants)
                .FirstOrDefaultAsync(x => x.CardNumber == code);
        }

        public async Task<List<Patient>> GetAll()
        {
            return await _dbContext.Patients.AsNoTracking().ToListAsync();
        }

        public async Task<Patient> GetById(int id)
        {
            return await _dbContext.Patients.AsNoTracking()
                .Include(x => x.Receipts)
                .Include(x => x.Records)
                .Include(x => x.Confidants)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Patient model)
        {
            if (!_dbContext.Patients.Any(x => x.Id == model.Id))
            {
                throw new BusinessLogicException($"Patient with id: {model.Id} doesn't exist");
            }

            _dbContext.Patients.Update(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePatientCode(int id, Guid code)
        {
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null)
            {
                throw new BusinessLogicException($"Patient with id: {id} doesn't exist");
            }

            patient.CardNumber = code;

            _dbContext.Patients.Update(patient);

            await _dbContext.SaveChangesAsync();
        }
    }
}
