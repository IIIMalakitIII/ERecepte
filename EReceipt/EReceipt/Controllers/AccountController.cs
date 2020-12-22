using AutoMapper;
using EReceipt.BLL.Interface;
using EReceipt.Common.Authentication;
using EReceipt.DAL.Entities;
using EReceipt.ViewModels.AccountViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EReceipt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(LoginViewModel model)
        {
            var token = await _accountService.SignIn(model.Email, model.Password);

            return Ok(new { token });
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            var userId = string.Empty;

            switch (model.Role)
            {
                case Role.Doctor:
                    var doctor = _mapper.Map<Doctor>(model.Doctor);
                    userId = await _accountService.SignUpDoctor(model.UserName, model.Email, model.Password, model.Role, doctor);
                    break;
                case Role.Patient:
                    var patient = _mapper.Map<Patient>(model.Patient);
                    userId = await _accountService.SignUpPatient(model.UserName, model.Email, model.Password, model.Role, patient);
                    break;
                case Role.Pharmacy:
                    var pharmacy = _mapper.Map<Pharmacy>(model.Pharmacy);
                    userId = await _accountService.SignUpPharmacy(model.UserName, model.Email, model.Password, model.Role, pharmacy);
                    break;
            }


            return Ok(new { userId });
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            await _accountService.ChangePassword(CurrentUser.UserId, model.CurrentPassword, model.NewPassword);

            return NoContent();
        }
    }
}
