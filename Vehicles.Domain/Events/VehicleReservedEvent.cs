using System;

namespace Vehicles.Domain.Events
{
    public record VehicleReservedEvent(
        Guid ReservationId,
        Guid VehicleId,
        DateTime PickupDate,
        DateTime ReturnDate
    );
}
