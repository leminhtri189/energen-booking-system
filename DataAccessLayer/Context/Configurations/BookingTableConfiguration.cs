using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Context.Configurations
{
    internal class BookingTableConfiguration: IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasOne(u => u.FeedbackNavigation)
                .WithOne(b => b.BookingNavigation)
                .HasForeignKey<Feedback>(b => b.BookingId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(u => u.TransactionNavigation)
                .WithOne(b => b.BookingNavigation)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
