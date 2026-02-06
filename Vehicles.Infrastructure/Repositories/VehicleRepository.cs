
using Microsoft.EntityFrameworkCore;
using Vehicles.Domain.Entities;
using Vehicles.Domain.Interfaces;
using Vehicles.Infrastructure.EF;

namespace Vehicles.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleContext _ctx;

        public VehicleRepository(VehicleContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<Vehicle>> GetAvailableByLocationAsync(Guid locationId)
        {
            return _ctx.Vehicles
                .Where(v =>
                    v.LocationId == locationId &&
                    v.Status == VehicleStatus.Available)
                .ToListAsync();
        }
    }
}
