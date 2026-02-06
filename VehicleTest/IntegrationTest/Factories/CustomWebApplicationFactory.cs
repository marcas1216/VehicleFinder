using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Vehicles.Infrastructure.EF;
using Vehicles.Domain.Interfaces;
using Vehicles.Infrastructure.Repositories;
using Vehicles.Application.Handlers; // Assembly de los handlers
using MediatR;
using VehicleFinder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Vehicles.Infrastructure.Mongo;

namespace VehicleTest.IntegrationTest.Factories
{
    public class CustomWebApplicationFactory
        : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {           
            Environment.SetEnvironmentVariable("INTEGRATIONTEST", "isTest");

            builder.UseEnvironment("IntegrationTest");

            builder.ConfigureServices(services =>
            {                
                var descriptors = services
                    .Where(d => d.ServiceType == typeof(DbContextOptions<VehicleContext>))
                    .ToList();
                foreach (var d in descriptors)
                    services.Remove(d);

                // 3️⃣ Registrar DbContext InMemory
                services.AddDbContext<VehicleContext>(options =>
                    options.UseInMemoryDatabase("VehicleTestDb"));

                // 4️⃣ Registrar repositorios y servicios necesarios
                services.AddScoped<IVehicleRepository, VehicleRepository>();
                services.AddScoped<IReservationRepository, ReservationRepository>();
                services.AddScoped<IMarketCatalogService, FakeMarketCatalogService>();

                // 5️⃣ Registrar MediatR **después de los repositorios**
                services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(SearchVehiclesQueryHandler).Assembly);
                });

                // 6️⃣ Inicializar base de datos limpia
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<VehicleContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                }
            });
        }
    }
}
