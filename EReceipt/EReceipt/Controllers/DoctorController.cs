using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EReceipt.BLL.Interface;
using EReceipt.DAL.Entities;
using EReceipt.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EReceipt.Controllers
{
    public class DoctorController : BaseApiController
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> Doctor()
        {
            var doctors = await _doctorService.GetAll();
            return Ok(doctors);
        }

        [HttpGet("doctors-by-instituation-id/{id}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> DoctorsByInstituation(int id)
        {
            var doctors = await _doctorService.GetDoctorByInstituationId(id);
            return doctors;
        }

        [HttpGet("doctors-by-instituation-autocomplete/{id}")]
        public async Task<ActionResult<IEnumerable<SelectViewModel>>>  DoctorsByInstituationAutocomplete(int id)
        {
            var doctors = await _doctorService.GetDoctorByInstituationId(id);
            var doctorViewModels = _mapper.Map<IEnumerable<SelectViewModel>>(doctors);

            return Ok(doctorViewModels);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<DoctorViewModel>> Doctor(int id)
        {
            var doctor = await _doctorService.GetById(id);
            var doctorViewModel = _mapper.Map<DoctorViewModel>(doctor);

            return doctorViewModel;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDoctor(DoctorViewModel model)
        {
            var doctor = _mapper.Map<Doctor>(model);

            await _doctorService.Update(doctor);

            return NoContent();
        }

    }
}
