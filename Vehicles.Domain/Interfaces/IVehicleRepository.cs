using Vehicles.Domain.Entities;

namespace Vehicles.Domain.Interfaces;

public interface IVehicleRepository
{
    Task<List<Vehicle>> GetAvailableByLocationAsync(Guid locationId);
}
