using System.Reflection;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public ApplicationDbContext() { }
        public DbSet<ServiceCategory> ServiceCategories { get; set; } = null!;

        public DbSet<Service> Services { get; set; } = null!;

        public DbSet<ServiceImage> ServiceImages { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Therapist> Therapists { get; set; } = null!;

        public DbSet<Booking> Bookings { get; set; } = null!;

        public DbSet<Feedback> Feedbacks { get; set; } = null!;

        public DbSet<Transaction> BookingTransactions { get; set; } = null!;

        public DbSet<SkinType> SkinTypes { get; set; } = null!;      

        public DbSet<Question> Questions { get; set; } = null!;

        public DbSet<QuestionOption> QuestionOptions { get; set; } = null!;

        public DbSet<Blog> Blogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
            return configuration["ConnectionStrings:SQLServer"];
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);

        }
    }
}