using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vehicles.Domain.Interfaces;
using Vehicles.Infrastructure.EF;
using Vehicles.Infrastructure.Mongo;
using Vehicles.Infrastructure.Repositories;

namespace Vehicles.Infrastructure.IoC
{
    public static class VehiclesServiceExtensionCollection
    {
        public static IServiceCollection AddVehiclesModule(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<VehicleContext>(o =>
                o.UseMySql(
                    config.GetConnectionString("mysql"),
                    ServerVersion.AutoDetect(
                        config.GetConnectionString("mysql"))));

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IMarketCatalogService, FakeMarketCatalogService>();
            
            return services;
        }
    }
}
