using Moq;
using FluentAssertions;
using Vehicles.Application.Handlers;
using Vehicles.Domain.Entities;
using Vehicles.Domain.Interfaces;
using Vehicles.Application.Queries;

public class SearchVehiclesQueryHandlerTests
{
    private readonly Mock<IVehicleRepository> _vehicleRepo = new();
    private readonly Mock<IReservationRepository> _reservationRepo = new();
    private readonly Mock<IMarketCatalogService> _marketService = new();

    private SearchVehiclesQueryHandler CreateHandler()
        => new(
            _vehicleRepo.Object,
            _reservationRepo.Object,
            _marketService.Object
        );

    private SearchVehiclesQuery TestQuery(Guid pickup)
        => new(
            pickup,
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow.AddDays(3)
        );

    [Fact]
    public async Task Should_Return_Empty_When_No_Vehicles_In_Location()
    {
        _vehicleRepo
            .Setup(r => r.GetAvailableByLocationAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<Vehicle>());

        var handler = CreateHandler();

        var result = await handler.Handle(TestQuery(Guid.NewGuid()), default);

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Should_Return_Empty_When_No_Allowed_Vehicle_Types()
    {
        var pickup = Guid.NewGuid();

        var vehicle = new VehicleBuilder()
            .WithLocation(pickup.ToString())
            .WithType("SUV")
            .Build();

        _vehicleRepo
            .Setup(r => r.GetAvailableByLocationAsync(pickup))
            .ReturnsAsync([vehicle]);

        _marketService
            .Setup(m => m.GetAllowedVehicleTypeIdsByLocation(pickup))
            .ReturnsAsync(new List<string>());

        var handler = CreateHandler();

        var result = await handler.Handle(TestQuery(pickup), default);

        result.Should().BeEmpty();
    }
       
    [Fact]
    public async Task Should_Exclude_Vehicles_With_Overlapping_Reservation()
    {
        var pickup = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();

        var vehicle = new VehicleBuilder()
            .WithId(vehicleId)
            .WithLocation(pickup.ToString())
            .WithType("SEDAN")
            .Build();

        _vehicleRepo
            .Setup(r => r.GetAvailableByLocationAsync(pickup))
            .ReturnsAsync([vehicle]);

        _marketService
            .Setup(m => m.GetAllowedVehicleTypeIdsByLocation(pickup))
            .ReturnsAsync(new List<string> { "SEDAN" });

        var reservation = new ReservationBuilder()
            .WithVehicle(vehicleId)
            .WithLocations(pickup.ToString(), pickup.ToString())
            .WithDates(
                DateTime.UtcNow.AddDays(1),
                DateTime.UtcNow.AddDays(3))
            .Build();

        _reservationRepo
            .Setup(r => r.GetActiveByVehicleIdsAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync([reservation]);

        var handler = CreateHandler();

        var query = new SearchVehiclesQuery(
            pickup,
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(2),
            DateTime.UtcNow.AddDays(4));

        var result = await handler.Handle(query, default);

        result.Should().BeEmpty();
    }
}
