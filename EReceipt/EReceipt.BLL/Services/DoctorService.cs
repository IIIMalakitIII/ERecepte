using EReceipt.BLL.Interface;
using EReceipt.Common.Exceptions;
using EReceipt.DAL.Context;
using EReceipt.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EReceipt.BLL.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _dbContext;

        public DoctorService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Doctor>> GetDoctorByInstituationId(int id)
        {
            return await _dbContext.Doctors.AsNoTracking()
                .Where(x => x.MedicalInstitutionId == id)
                .OrderByDescending(x => x.LastName)
                .ToListAsync();
        }

        public async Task<List<Doctor>> GetAll()
        {
            return await _dbContext.Doctors
                .AsNoTracking()
                .OrderByDescending(x => x.LastName)
                .ToListAsync();
        }

        public async Task<Doctor> GetById(int id)
        {
            return await _dbContext.Doctors.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Doctor model)
        {
            if (!_dbContext.Doctors.Any(x => x.Id == model.Id))
            {
                throw new BusinessLogicException($"Doctor with id: {model.Id} doesn't exist");
            }

            _dbContext.Doctors.Update(model);

            await _dbContext.SaveChangesAsync();
        }
    }
}
