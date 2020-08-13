using Carpark.Register.Application.Common.Interfaces;
using Carpark.Register.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Carpark.Register.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<RatesDbContext>(options =>
                    options.UseInMemoryDatabase("Carpark.RegisterDb"));
            }
            else
            {
                services.AddDbContext<RatesDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(RatesDbContext).Assembly.FullName)));
            }

            services.AddScoped<IRatesDbContext>(provider => provider.GetService<RatesDbContext>());

            return services;
        }
    }
}
