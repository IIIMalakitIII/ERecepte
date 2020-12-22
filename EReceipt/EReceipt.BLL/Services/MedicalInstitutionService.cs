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
    public class MedicalInstitutionService: IMedicalInstitutionService
    {
        private readonly AppDbContext _dbContext;

        public MedicalInstitutionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MedicalInstitution> GetInstitutionByDoctorId(int id)
        {
            return await _dbContext.MedicalInstitutions.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Doctors.Any(y => y.Id == id));
        }

        public async Task<List<MedicalInstitution>> GetAll()
        {
            return await _dbContext.MedicalInstitutions
                .AsNoTracking()
                .OrderByDescending(x => x.Name)
                .ToListAsync();
        }

        public async Task<MedicalInstitution> GetById(int id)
        {
            return await _dbContext.MedicalInstitutions.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Create(MedicalInstitution model)
        {
            await _dbContext.MedicalInstitutions.AddAsync(model);

            await _dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task Update(MedicalInstitution model)
        {
            if (!_dbContext.MedicalInstitutions.Any(x => x.Id == model.Id))
            {
                throw new BusinessLogicException($"Institution with id: {model.Id} doesn't exist");
            } 

            _dbContext.MedicalInstitutions.Update(model);

            await _dbContext.SaveChangesAsync();
        }
    }
}
