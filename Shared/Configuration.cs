using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Shared.Token;
using Shared.Email;

namespace Shared
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureSharedLibrary(this IServiceCollection services, IConfiguration configurations)
        {
            services.AddScoped<ITokenUtilities, TokenUtilities>();

            services.AddScoped<IEmailUtilities, EmailUtilities>();
           // var PayPal = configurations.GetSection("PayPal");
          //  services.<PayPal>(PayPal);
            return services;
        }
    }
}
