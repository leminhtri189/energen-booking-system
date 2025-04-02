using BusinessLogicLayer.Services.Implementation;
using BusinessLogicLayer.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISkinTimeService, SkinTimeService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ITherapistService, TherapistService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICategotiryService, CategoryService>();
            services.AddScoped<ISkinTypeService, SkinTypeService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.Configure<PayPal>(options => configuration.GetSection("PayPal").Bind(options));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<PayPal>>().Value);
            return services;
        }
    }
}
