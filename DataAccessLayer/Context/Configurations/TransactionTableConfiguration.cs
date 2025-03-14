using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context.Configurations
{
    public class TransactionTableConfiguration: IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(x => x.BookingNavigation)
                .WithOne(x => x.TransactionNavigation)
                .HasForeignKey<Transaction>(x => x.BookingId);
        }
    }
}
