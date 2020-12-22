using System;
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
    public class PatientController : BaseApiController
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientViewModel>>> Patient()
        {
            var patients = await _patientService.GetAll();
            var patientsViewModel = _mapper.Map<IEnumerable<PatientViewModel>>(patients);

            return Ok(patientsViewModel);
        }

        [HttpGet("patient-by-code/{id}")]
        public async Task<ActionResult<PatientViewModel>> PatientByCode(Guid code)
        {
            var patient = await _patientService.GetPatientByCode(code);
            var patientViewModel = _mapper.Map<PatientViewModel>(patient);
            return patientViewModel;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<PatientViewModel>> Patient(int id)
        {
            var patient = await _patientService.GetById(id);
            var patientViewModel = _mapper.Map<PatientViewModel>(patient);
            return patientViewModel;
        }

        [HttpPut("update-patient-info")]
        public async Task<IActionResult> UpdatePatient(PatientViewModel model)
        {
            var patient = _mapper.Map<Patient>(model);

            await _patientService.Update(patient);

            return NoContent();
        }

        [HttpPut("update-patient-code")]
        public async Task<IActionResult> UpdatePatientCode(int id, Guid newCode)
        {
            await _patientService.UpdatePatientCode(id, newCode);

            return NoContent();
        }
    }
}
