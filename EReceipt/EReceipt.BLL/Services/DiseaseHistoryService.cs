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
    public class DiseaseHistoryService: IDiseaseHistoryService
    {
        private readonly AppDbContext _dbContext;

        public DiseaseHistoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DiseaseHistory> GetDiseaseHistoryByPatientId(int id)
        {
            return await _dbContext.DiseaseHistories.AsNoTracking()
                .FirstOrDefaultAsync(x => x.PatientId == id);
        }

        public async Task<List<DiseaseHistory>> GetAll()
        {
            return await _dbContext.DiseaseHistories.AsNoTracking().ToListAsync();
        }

        public async Task<DiseaseHistory> GetById(int id)
        {
            return await _dbContext.DiseaseHistories.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Create(DiseaseHistory model)
        {
            await _dbContext.DiseaseHistories.AddAsync(model);

            await _dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task Update(DiseaseHistory model)
        {
            if (!_dbContext.DiseaseHistories.Any(x => x.Id == model.Id))
            {
                throw new BusinessLogicException($"DiseaseHistory with id: {model.Id} doesn't exist");
            }

            _dbContext.DiseaseHistories.Update(model);

            await _dbContext.SaveChangesAsync();
        }
    }
}
