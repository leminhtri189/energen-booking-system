using AutoMapper;
using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Email;
using Shared.Token;

namespace Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _service;
        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> TransactionCallbackAsync(string? key)
        {
            var data = Request.Query;
            var sussess = await _service.CallbackPayment(key, data);
            if (sussess)
            {
                return View("PaymentSusscess");
            }
            return View("PaymentFailed");
        }
    }
}
