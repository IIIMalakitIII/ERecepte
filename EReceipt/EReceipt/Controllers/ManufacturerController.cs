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
    public class ManufacturerController : BaseApiController
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly IMapper _mapper;

        public ManufacturerController(IManufacturerService manufacturerService, IMapper mapper)
        {
            _manufacturerService = manufacturerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManufacturerViewModel>>> Manufacturer()
        {
            var manufacturers = await _manufacturerService.GetAll();
            var manufacturersViewModel = _mapper.Map<IEnumerable<ManufacturerViewModel>>(manufacturers);

            return Ok(manufacturersViewModel);
        }

        [HttpGet("autocomplete")]
        public async Task<ActionResult<IEnumerable<SelectViewModel>>> Autocomplete()
        {
            var manufacturers = await _manufacturerService.GetAll();
            var manufacturersViewModel = _mapper.Map<IEnumerable<SelectViewModel>>(manufacturers);

            return Ok(manufacturersViewModel);
        }

        [HttpGet("manufacturer-by-medicament-id/{id}")]
        public async Task<ActionResult<ManufacturerViewModel>> ManufacturerByMedicament(int id)
        {
            var manufacturer = await _manufacturerService.GetManufacturerByMedicamentId(id);
            var manufacturerViewModel = _mapper.Map<ManufacturerViewModel>(manufacturer);

            return manufacturerViewModel;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<ManufacturerViewModel>> Manufacturer(int id)
        {
            var manufacturer = await _manufacturerService.GetById(id);
            var manufacturerViewModel = _mapper.Map<ManufacturerViewModel>(manufacturer);

            return manufacturerViewModel;
        }


        [HttpPost]
        public async Task<IActionResult> Manufacturer([FromBody] ManufacturerViewModel model)
        {
            var manufacturer = _mapper.Map<Manufacturer>(model);
            var newCreatedId = await _manufacturerService.Create(manufacturer);

            return Ok(new { newCreatedId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateManufacturer([FromBody] ManufacturerViewModel model)
        {
            var manufacturer = _mapper.Map<Manufacturer>(model);

            await _manufacturerService.Update(manufacturer);

            return NoContent();
        }
    }
}
