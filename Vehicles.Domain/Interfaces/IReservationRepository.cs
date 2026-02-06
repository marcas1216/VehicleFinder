using Vehicles.Domain.Entities;

namespace Vehicles.Domain.Interfaces;

public interface IReservationRepository
{
    Task<List<Reservation>> GetActiveByVehicleIdsAsync(List<Guid> vehicleIds);
}
