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
    public class MedicamentCategoryService: IMedicamentCategoryService
    {
        private readonly AppDbContext _dbContext;

        public MedicamentCategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MedicamentCategory> GetMedicamentCategoryByMedicamentId(int id)
        {
            return await _dbContext.MedicamentCategories.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Medicaments.Any(y => y.Id == id));
        }

        public async Task<List<MedicamentCategory>> GetAll()
        {
            return await _dbContext.MedicamentCategories
                .AsNoTracking()
                .OrderByDescending(x => x.Name)
                .ToListAsync();
        }

        public async Task<MedicamentCategory> GetById(int id)
        {
            return await _dbContext.MedicamentCategories.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Create(MedicamentCategory model)
        {
            await _dbContext.MedicamentCategories.AddAsync(model);

            await _dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task CreateRange(List<string> model)
        {
            var newCategory = new List<MedicamentCategory>();
            var categoryList = await _dbContext.MedicamentCategories.ToListAsync();
            if (categoryList != null)
            {
                model.ForEach(x =>
                {
                    if (!categoryList.Any(o => o.Name.ToLower() == x.ToLower()))
                    {
                        newCategory.Add(new MedicamentCategory()
                        {
                            Name = x
                        });
                    }
                });
            }
            else
            {
                model.ForEach(x =>
                {
                    newCategory.Add(new MedicamentCategory()
                    {
                        Name = x
                    });
                });
            }

            await _dbContext.MedicamentCategories.AddRangeAsync(newCategory);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(MedicamentCategory model)
        {
            if (!_dbContext.MedicamentCategories.Any(x => x.Id == model.Id))
            {
                throw new BusinessLogicException($"Medicament Category with id: {model.Id} doesn't exist");
            }

            _dbContext.MedicamentCategories.Update(model);

            await _dbContext.SaveChangesAsync();
        }
    }
}
