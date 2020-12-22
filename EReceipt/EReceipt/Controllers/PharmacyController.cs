using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EReceipt.BLL.Interface;
using EReceipt.DAL.Entities;
using EReceipt.ViewModels;

namespace EReceipt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : BaseApiController
    {
        private readonly IPharmacyService _pharmacyService;
        private readonly IMapper _mapper;

        public PharmacyController(IPharmacyService pharmacyService, IMapper mapper)
        {
            _pharmacyService = pharmacyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PharmacyViewModel>>> Pharmacy()
        {
            var pharmacys = await _pharmacyService.GetAll();
            var pharmacysViewModel = _mapper.Map<IEnumerable<PharmacyViewModel>>(pharmacys);

            return Ok(pharmacysViewModel);
        }

        [HttpGet("pharmacy-by-medicament-id/{id}")]
        public async Task<ActionResult<PharmacyViewModel>> PharmacyByMedicament(int id)
        {
            var pharmacy = await _pharmacyService.GetPharmacyByMedicamentId(id);
            var pharmacyViewModel = _mapper.Map<PharmacyViewModel>(pharmacy);

            return pharmacyViewModel;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<PharmacyViewModel>> Pharmacy(int id)
        {
            var pharmacy = await _pharmacyService.GetById(id);
            var pharmacyViewModel = _mapper.Map<PharmacyViewModel>(pharmacy);

            return pharmacyViewModel;
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePharmacy(PharmacyViewModel model)
        {
            var pharmacy = _mapper.Map<Pharmacy>(model);

            await _pharmacyService.Update(pharmacy);

            return NoContent();
        }
    }
}
