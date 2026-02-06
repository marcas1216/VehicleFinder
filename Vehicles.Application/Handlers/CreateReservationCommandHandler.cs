using MediatR;
using Vehicles.Domain.Entities;
using Vehicles.Domain.Events;
using Vehicles.Domain.Interfaces;
using Vehicles.Application.Commands;

namespace Vehicles.Application.Handlers
{
    public class CreateReservationCommandHandler
        : IRequestHandler<CreateReservationCommand, Guid>
    {
        private readonly IReservationCommandRepository _reservationRepo;

        public static event Action<VehicleReservedEvent>? VehicleReserved;

        public CreateReservationCommandHandler(
            IReservationCommandRepository reservationRepo)
        {
            _reservationRepo = reservationRepo;
        }

        public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken ct)
        {            
            var reservation = new Reservation(
                Guid.NewGuid(),
                request.VehicleId,
                request.PickupDate,
                request.ReturnDate,
                request.PickupLocationCode,
                request.ReturnLocationCode
            );
                      
            await _reservationRepo.AddAsync(reservation);

            VehicleReserved?.Invoke(new VehicleReservedEvent(
                reservation.Id,
                reservation.VehicleId,
                reservation.PickupDateTime,
                reservation.ReturnDateTime
            ));

            return reservation.Id;
        }
    }
}
