using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using DataAccessLayer.Context;
using DataAccessLayer.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PayPal _payPal;
        public TransactionService(IUnitOfWork unitOfWork,PayPal payPal
       )
        {
            _unitOfWork = unitOfWork;
            _payPal = payPal;
        }
        public async Task<bool> CallbackPayment(string? key, IQueryCollection data)
        {
            Guid bookingId = Guid.Parse(key);
            var booking = await _unitOfWork.GenericRepository<Booking>().GetFirstAsync(bo => bo.Id == bookingId);
            var token = data["token"].ToString();
            var captureResponse = await _payPal.SendCaptureRequest(token);
            if(captureResponse.GetProperty("status").GetString().ToLower() == "completed")
            {
               await _unitOfWork.Transactions.CreateAsync(booking,true);
                return true;
            }
            await _unitOfWork.Transactions.CreateAsync(booking, false);
            return false;            
        }
    }
}
