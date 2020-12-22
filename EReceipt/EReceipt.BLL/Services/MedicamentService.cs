using EReceipt.BLL.Interface;
using EReceipt.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EReceipt.Common.Exceptions;
using EReceipt.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace EReceipt.BLL.Services
{
    public class MedicamentService: IMedicamentService
    {

        private readonly AppDbContext _dbContext;

        public MedicamentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Medicament>> GetMedicamentByCategoryId(int id)
        {
            return await _dbContext.Medicaments.AsNoTracking()
                    .Include(x => x.MedicamentCategory)
                    .Include(x => x.Manufacturer)
                .Where(x => x.CategoryId == id).ToListAsync();
        }

        public async Task<List<Medicament>> GetMedicamentByManufactorId(int id)
        {
            return await _dbContext.Medicaments.AsNoTracking()
                .Where(x => x.ManufacturerId == id).ToListAsync();
        }

        public async Task<List<Medicament>> GetMedicamentByPharmacyId(int id)
        {
            var medicamentIds = await _dbContext.MedicamentPharmacy
                .Where(x => x.PharmacyId == id)
                .Select(x => x.MedicamentId)
                .ToListAsync();

            return await _dbContext.Medicaments.AsNoTracking()
                .Where(x => medicamentIds.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<Medicament>> GetAll()
        {
            return await _dbContext.Medicaments
                .AsNoTracking()
                .Include(x => x.MedicamentCategory)
                .Include(x => x.Manufacturer)
                .ToListAsync();
        }

        public async Task<Medicament> GetById(int id)
        {
            return await _dbContext.Medicaments.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Create(Medicament model)
        {
            await _dbContext.Medicaments.AddAsync(model);

            await _dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task Update(Medicament model)
        {
            if (!_dbContext.MedicalInstitutions.Any(x => x.Id == model.Id))
            {
                throw new BusinessLogicException($"Medicament with id: {model.Id} doesn't exist");
            }

            _dbContext.Medicaments.Update(model);

            await _dbContext.SaveChangesAsync();
        }


    }
}
