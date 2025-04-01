using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation
{
    public class TransactionRepo : GenericRepository<Transaction>, ITransactionRepo
    {
        public TransactionRepo(ApplicationDbContext context) : base(context)
        {

        }

        public async Task CreateAsync(Booking booking,bool isSuscess)
        {
            PaymentStatus status;
            if (isSuscess)
            {
                status = PaymentStatus.Sussces;
            }
            else {
                status = PaymentStatus.Failed;
            }
            var transaction = new Transaction();
            transaction.TransactionTime = DateTime.Now;
            transaction.IsRefund = false;
            transaction.Amount = booking.ServiceNavigation.Price;
            transaction.Id = Guid.NewGuid();
            transaction.Status = status;
            transaction.BookingId = booking.Id;

            await ((ApplicationDbContext)context).AddAsync(transaction);
            await ((ApplicationDbContext)context).SaveChangesAsync();
        }
    }
}
