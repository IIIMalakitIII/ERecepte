using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using EReceipt.BLL.Interface;
using EReceipt.DAL.Entities;
using EReceipt.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EReceipt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentController : BaseApiController
    {
        private readonly IMedicamentService _medicamentService;
        private readonly IMapper _mapper;

        public MedicamentController(IMedicamentService medicamentService, IMapper mapper)
        {
            _medicamentService = medicamentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicamentViewModel>>> Medicament()
        {
            var medicaments = await _medicamentService.GetAll();
            var medicamentsViewModel = _mapper.Map<IEnumerable<MedicamentViewModel>>(medicaments);

            return Ok(medicamentsViewModel);
        }

        [HttpGet("medicament-by-category-id/{id}")]
        public async Task<ActionResult<IEnumerable<MedicamentViewModel>>> MedicamentByCategory(int id)
        {
            var medicament = await _medicamentService.GetMedicamentByCategoryId(id);
            var medicamentViewModel = _mapper.Map<IEnumerable<MedicamentViewModel>>(medicament);

            return Ok(medicamentViewModel);
        }

        [HttpGet("medicament-by-pharmacy-id/{id}")]
        public async Task<ActionResult<IEnumerator<MedicamentViewModel>>> MedicamentByPharmacy(int id)
        {
            var medicament = await _medicamentService.GetMedicamentByPharmacyId(id);
            var medicamentViewModel = _mapper.Map<IEnumerable<MedicamentViewModel>>(medicament);

            return Ok(medicamentViewModel);
        }

        [HttpGet("medicament-by-manufactor-id/{id}")]
        public async Task<ActionResult<IEnumerator<MedicamentViewModel>>> MedicamentByManufactor(int id)
        {
            var medicament = await _medicamentService.GetMedicamentByManufactorId(id);
            var medicamentViewModel = _mapper.Map<MedicamentViewModel>(medicament);

            return Ok(medicamentViewModel);
        }


        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<MedicamentViewModel>> Medicament(int id)
        {
            var medicament = await _medicamentService.GetById(id);
            var medicamentViewModel = _mapper.Map<MedicamentViewModel>(medicament);

            return medicamentViewModel;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Medicament([FromForm] MedicamentViewModel model)
        {
            model.MedicamentCategory = null;
            var medicament = _mapper.Map<Medicament>(model);

            medicament.Picture = ConvertFileToBytes(model.Picture);
            medicament.ContentType = model.Picture?.ContentType;
            var newCreatedId = await _medicamentService.Create(medicament);

            return Ok(new { newCreatedId });
        }

        [HttpPut]
        [DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateMedicament([FromForm] MedicamentViewModel model)
        {
            var medicament = _mapper.Map<Medicament>(model);

            medicament.Picture = ConvertFileToBytes(model.Picture);
            medicament.ContentType = model.Picture?.ContentType;

            await _medicamentService.Update(medicament);

            return NoContent();
        }

        private byte[] ConvertFileToBytes(IFormFile file)
        {
            if (file is null)
            {
                return null;
            }

            using var ms = new MemoryStream();

            file.CopyTo(ms);

            return ms.ToArray();
        }
    }
}
