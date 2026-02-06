using Vehicles.Application.Queries;

namespace VehicleFinder.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatR(
            this IServiceCollection services,
            IConfiguration config)
        {            

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(
                    typeof(SearchVehiclesQuery).Assembly));

            return services;
        }
    }
}
