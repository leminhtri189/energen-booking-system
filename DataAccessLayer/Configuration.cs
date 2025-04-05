using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Implementation;
using DataAccessLayer.Repositories.Interface;
using DataAccessLayer.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.File;

namespace DataAccessLayer
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SQLServer")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ITherapistRepository, TherapistRepository>();
            services.AddSingleton<FirebaseStorage>();

            return services;
        }
    }
}
