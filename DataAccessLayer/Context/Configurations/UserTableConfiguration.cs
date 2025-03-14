using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Context.Configurations
{
    internal class UserTableConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Therapists)
                .WithOne(b => b.UserNavigation)
                .HasForeignKey<Therapist>(b => b.UserId);
        }
    }
}
