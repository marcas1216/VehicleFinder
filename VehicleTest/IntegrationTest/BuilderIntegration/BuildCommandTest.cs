using Vehicles.Infrastructure.EF;
using Vehicles.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace VehicleTest.IntegrationTest.BuilderIntegration
{
    public static class BuildCommandTest
    {       
        public static (VehicleContext db, Guid pickupLocationId, Guid returnLocationId) SeedVehicles(VehicleContext db)
        {
            db.Vehicles.RemoveRange(db.Vehicles);
                       
            var pickupLocationId = Guid.NewGuid();
            var returnLocationId = Guid.NewGuid();

            var vehicles = new List<Vehicle>
            {
                new VehicleBuilder()
                    .WithType("SEDAN")
                    .WithLocation("LOC1")
                    .Build(),
                new VehicleBuilder()
                    .WithType("SUV")
                    .WithLocation("LOC2")
                    .Build(),
                new VehicleBuilder()
                    .WithType("HATCHBACK")
                    .WithLocation("LOC3")
                    .Build()
            };

            vehicles.ForEach(v => v.LocationId = pickupLocationId);

            db.Vehicles.AddRange(vehicles);
            db.SaveChanges();

            return (db, pickupLocationId, returnLocationId);
        }

        public static VehicleContext GetDbContext(IServiceProvider services)
        {
            return services.GetRequiredService<VehicleContext>();
        }
    }
}
