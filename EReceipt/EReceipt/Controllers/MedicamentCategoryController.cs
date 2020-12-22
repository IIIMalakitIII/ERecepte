using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EReceipt.BLL.Interface;
using EReceipt.DAL.Entities;
using EReceipt.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EReceipt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentCategoryController : BaseApiController
    {
        private readonly IMedicamentCategoryService _medicamentCategoryService;
        private readonly IMapper _mapper;

        public MedicamentCategoryController(IMedicamentCategoryService medicamentCategoryService, IMapper mapper)
        {
            _medicamentCategoryService = medicamentCategoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicamentCategoryViewModel>>> MedicamentCategory()
        {
            var medicamentCategories = await _medicamentCategoryService.GetAll();
            var medicamentCategoriesViewModel = _mapper.Map<IEnumerable<MedicamentCategoryViewModel>>(medicamentCategories);

            return Ok(medicamentCategoriesViewModel);
        }

        [HttpGet("medicament-category-by-medicament-id/{id}")]
        public async Task<ActionResult<MedicamentCategoryViewModel>> MedicamentCategoryByMedicament(int id)
        {
            var medicamentCategory = await _medicamentCategoryService.GetMedicamentCategoryByMedicamentId(id);
            var medicamentCategoryViewModel = _mapper.Map<MedicamentCategoryViewModel>(medicamentCategory);

            return medicamentCategoryViewModel;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<MedicamentCategoryViewModel>> MedicamentCategory(int id)
        {
            var medicamentCategory = await _medicamentCategoryService.GetById(id);
            var medicamentCategoryViewModel = _mapper.Map<MedicamentCategoryViewModel>(medicamentCategory);

            return medicamentCategoryViewModel;
        }


        [HttpPost]
        public async Task<IActionResult> MedicamentCategory([FromBody] MedicamentCategoryViewModel model)
        {
            var medicamentCategory = _mapper.Map<MedicamentCategory>(model);
            var newCreatedId = await _medicamentCategoryService.Create(medicamentCategory);

            return Ok(new { newCreatedId });
        }

        [HttpPost("category-list")]
        public async Task<IActionResult> MedicamentCategoryList(List<string> model)
        {
            await _medicamentCategoryService.CreateRange(model);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMedicamentCategory([FromBody] MedicamentCategoryViewModel model)
        {
            var medicamentCategory = _mapper.Map<MedicamentCategory>(model);

            await _medicamentCategoryService.Update(medicamentCategory);

            return NoContent();
        }
    }
}
