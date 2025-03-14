using BusinessLogicLayer.Services.Implementation;
using BusinessLogicLayer.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            return services;
        }
    }
}
