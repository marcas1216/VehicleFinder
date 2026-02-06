
using Microsoft.EntityFrameworkCore;
using Vehicles.Domain.Entities;
using Vehicles.Domain.Interfaces;
using Vehicles.Infrastructure.EF;

namespace Vehicles.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly VehicleContext _ctx;

        public ReservationRepository(VehicleContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<Reservation>> GetActiveByVehicleIdsAsync(List<Guid> ids)
        {
            return _ctx.Reservations
                .Where(r =>
                    ids.Contains(r.VehicleId) &&
                    r.Status == ReservationStatus.Active)
                .ToListAsync();
        }
    }
}
