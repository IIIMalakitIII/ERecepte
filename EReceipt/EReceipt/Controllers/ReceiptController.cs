using AutoMapper;
using EReceipt.BLL.Interface;
using EReceipt.DAL.Entities;
using EReceipt.DAL.Enums;
using EReceipt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EReceipt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : BaseApiController
    {
        private readonly IReceiptService _receiptService;
        private readonly IMapper _mapper;

        public ReceiptController(IReceiptService receiptService, IMapper mapper)
        {
            _receiptService = receiptService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptViewModel>>> Receipt()
        {
            var receipts = await _receiptService.GetAll();
            var receiptsViewModel = _mapper.Map<IEnumerable<ReceiptViewModel>>(receipts);

            return Ok(receiptsViewModel);
        }

        [HttpGet("receipt-by-patient-id/{id}")]
        public async Task<ActionResult<IEnumerable<ReceiptViewModel>>> ReceiptsByPatients(int id)
        {
            var receipt = await _receiptService.GetReceiptsByPatientsId(id);
            var receiptViewModel = _mapper.Map<IEnumerable<ReceiptViewModel>>(receipt);

            return Ok(receiptViewModel);
        }

        [HttpGet("receipt-by-doctor-id/{id}")]
        public async Task<ActionResult<IEnumerator<ReceiptViewModel>>> ReceiptsByDoctor(int id)
        {
            var receipt = await _receiptService.GetReceiptsByDoctorId(id);
            var receiptViewModel = _mapper.Map<IEnumerable<ReceiptViewModel>>(receipt);

            return Ok(receiptViewModel);
        }

        [HttpGet("receipt-by-patient-active-id/{id}")]
        public async Task<ActionResult<IEnumerator<ReceiptViewModel>>> ReceiptByPatientActive(int id)
        {
            var receipt = await _receiptService.GetReceiptByPatientActiveId(id);
            var receiptViewModel = _mapper.Map<ReceiptViewModel>(receipt);

            return Ok(receiptViewModel);
        }

        [HttpGet("get-my-receipts")]
        public async Task<ActionResult<IEnumerator<ReceiptViewModel>>> MyReceipts()
        {
            var receipt = await _receiptService.GetUserReceipts(CurrentUser.UserId, CurrentUser.Role);
            var receiptViewModel = _mapper.Map<List<ReceiptViewModel>>(receipt);

            return Ok(receiptViewModel);
        }


        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<ReceiptViewModel>> Receipt(int id)
        {
            var receipt = await _receiptService.GetById(id);
            var receiptViewModel = _mapper.Map<ReceiptViewModel>(receipt);

            return receiptViewModel;
        }

        [HttpPost]
        public async Task<IActionResult> Medicament([FromBody] ReceiptViewModel model)
        {
            var receipt = _mapper.Map<Receipt>(model);
            var newCreatedId = await _receiptService.Create(receipt, CurrentUser.UserId);

            return Ok(new { newCreatedId });
        }

        [HttpPut("update-receipt-info")]
        public async Task<IActionResult> UpdateReceipt(ReceiptViewModel model)
        {
            var receipt = _mapper.Map<Receipt>(model);

            await _receiptService.Update(receipt);

            return NoContent();
        }

        [HttpPut("update-receipt-status")]
        public async Task<IActionResult> UpdateReceiptStatus(int id, ReceiptStatus status)
        {
            await _receiptService.UpdateReceiptStatus(id, status);

            return NoContent();
        }
    }
}
