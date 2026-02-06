
using Vehicles.Domain.Entities;

namespace Vehicles.Domain.Interfaces
{
    public interface IReservationCommandRepository
    {
        Task AddAsync(Reservation reservation);
    }
}
