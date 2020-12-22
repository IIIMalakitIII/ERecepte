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
    public class PharmacyService: IPharmacyService
    {
        private readonly AppDbContext _dbContext;

        public PharmacyService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Pharmacy> GetPharmacyByMedicamentId(int id)
        {

            var pharmacyId = await _dbContext.MedicamentPharmacy
                .Where(x => x.MedicamentId == id)
                .Select(x => x.PharmacyId)
                .FirstOrDefaultAsync();

            return await _dbContext.Pharmacies.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == pharmacyId);
        }

        public async Task<List<Pharmacy>> GetAll()
        {
            return await _dbContext.Pharmacies.AsNoTracking().ToListAsync();
        }

        public async Task<Pharmacy> GetById(int id)
        {
            return await _dbContext.Pharmacies.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Pharmacy model)
        {
            if (!_dbContext.Pharmacies.Any(x => x.Id == model.Id))
            {
                throw new BusinessLogicException($"Pharmacy with id: {model.Id} doesn't exist");
            }

            _dbContext.Pharmacies.Update(model);

            await _dbContext.SaveChangesAsync();
        }
    }
}
