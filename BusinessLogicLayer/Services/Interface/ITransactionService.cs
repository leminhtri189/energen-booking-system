using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface ITransactionService
    {
        Task<bool> CallbackPayment(string key, IQueryCollection data);
    }
}
