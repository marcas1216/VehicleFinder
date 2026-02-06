using MediatR;

namespace Vehicles.Application.Commands
{
    public record CreateReservationCommand(
        Guid VehicleId,
        Guid CustomerId,
        DateTime PickupDate,
        DateTime ReturnDate,
        string PickupLocationCode,
        string ReturnLocationCode
    ) : IRequest<Guid>;
}
