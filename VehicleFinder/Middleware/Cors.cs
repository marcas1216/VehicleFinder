using Microsoft.Extensions.Options;

namespace VehicleFinder.Middleware
{
    public static class Cors
    {
        public static IServiceCollection AddInmoCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("OutletRentalCars", policy =>
                {
                    policy
                        .WithOrigins("https://www.outletrentalcars.com")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });

                options.AddPolicy("LocalDev", policy =>
                {
                    policy
                        .WithOrigins(
                            "https://localhost:7085"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
                    
            return services;
        }
    }
}
