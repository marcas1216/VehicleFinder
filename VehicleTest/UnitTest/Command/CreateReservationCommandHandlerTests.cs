using Moq;
using FluentAssertions;
using Vehicles.Application.Handlers;
using Vehicles.Application.Commands;
using Vehicles.Domain.Entities;
using Vehicles.Domain.Events;
using Vehicles.Domain.Interfaces;

public class CreateReservationCommandHandlerTests
{
    private readonly Mock<IReservationCommandRepository> _reservationRepo = new();

    private CreateReservationCommandHandler CreateHandler()
        => new(_reservationRepo.Object);

    [Fact]
    public async Task Should_Create_Reservation_And_Raise_Event()
    {        
        VehicleReservedEvent? raisedEvent = null;
        CreateReservationCommandHandler.VehicleReserved += e => raisedEvent = e;

        var handler = CreateHandler();

        var builder = new ReservationCommandBuilder()
            .WithVehicle(Guid.NewGuid())
            .WithDates(DateTime.UtcNow, DateTime.UtcNow.AddDays(1))
            .WithLocations("LOC1", "LOC2");

        var command = new CreateReservationCommand(
            builder.Build().VehicleId,
            Guid.NewGuid(),
            builder.Build().PickupDateTime,
            builder.Build().ReturnDateTime,
            builder.Build().PickupLocationCode,
            builder.Build().ReturnLocationCode
        );

        var reservationId = await handler.Handle(command, default);

        reservationId.Should().NotBeEmpty();
        _reservationRepo.Verify(r => r.AddAsync(It.Is<Reservation>(
            r => r.Id == reservationId &&
                 r.VehicleId == command.VehicleId &&
                 r.PickupLocationCode == "LOC1" &&
                 r.ReturnLocationCode == "LOC2"
        )), Times.Once);
              
        raisedEvent.Should().NotBeNull();
        raisedEvent!.ReservationId.Should().Be(reservationId);
        raisedEvent.VehicleId.Should().Be(command.VehicleId);
                
        CreateReservationCommandHandler.VehicleReserved -= e => raisedEvent = e;
    }

    [Fact]
    public async Task Should_Throw_If_ReturnDate_Before_PickupDate()
    {
        var handler = CreateHandler();
                
        var pickup = DateTime.UtcNow.AddDays(1);
        var returnDate = DateTime.UtcNow; 
        var vehicleId = Guid.NewGuid();

        var command = new CreateReservationCommand(
            vehicleId,
            Guid.NewGuid(),
            pickup,
            returnDate,
            "LOC1",
            "LOC2"
        );

        Func<Task> act = async () => await handler.Handle(command, default);

        await act.Should().ThrowAsync<ArgumentException>()
                 .WithMessage("Return date must be greater than pickup date");
    }
}
