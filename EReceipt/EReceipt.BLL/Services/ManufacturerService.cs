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
    public class ManufacturerService: IManufacturerService
    {
        private readonly AppDbContext _dbContext;

        public ManufacturerService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Manufacturer> GetManufacturerByMedicamentId(int id)
        {
            return await _dbContext.Manufacturers.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Medicaments.Any(y => y.Id == id));
        }

        public async Task<List<Manufacturer>> GetAll()
        {
            return await _dbContext.Manufacturers
                .AsNoTracking()
                .OrderByDescending(x => x.Name)
                .ToListAsync();
        }

        public async Task<Manufacturer> GetById(int id)
        {
            return await _dbContext.Manufacturers.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Create(Manufacturer model)
        {
            await _dbContext.Manufacturers.AddAsync(model);

            await _dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task Update(Manufacturer model)
        {
            if (!_dbContext.Manufacturers.Any(x => x.Id == model.Id))
            {
                throw new BusinessLogicException($"Manufacturer with id: {model.Id} doesn't exist");
            }

            _dbContext.Manufacturers.Update(model);

            await _dbContext.SaveChangesAsync();
        }
    }
}
