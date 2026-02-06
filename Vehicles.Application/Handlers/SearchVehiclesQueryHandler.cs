using MediatR;
using Vehicles.Application.Dto;
using Vehicles.Application.Queries;
using Vehicles.Domain.Interfaces;

namespace Vehicles.Application.Handlers;

public class SearchVehiclesQueryHandler
    : IRequestHandler<SearchVehiclesQuery, List<VehicleResultDto>>
{
    private readonly IVehicleRepository _vehicleRepo;
    private readonly IReservationRepository _reservationRepo;
    private readonly IMarketCatalogService _marketService;

    public SearchVehiclesQueryHandler(
        IVehicleRepository vehicleRepo,
        IReservationRepository reservationRepo,
        IMarketCatalogService marketService)
    {
        _vehicleRepo = vehicleRepo;
        _reservationRepo = reservationRepo;
        _marketService = marketService;
    }

    public async Task<List<VehicleResultDto>> Handle(
        SearchVehiclesQuery request,
        CancellationToken ct)
    {       
        var vehicles = await _vehicleRepo
            .GetAvailableByLocationAsync(request.PickupLocationId);

        if (!vehicles.Any()) return [];
               
        var allowedTypes = await _marketService
            .GetAllowedVehicleTypeIdsByLocation(request.PickupLocationId);

        vehicles = vehicles
            .Where(v => allowedTypes.Contains(v.VehicleTypeId))
            .ToList();

        if (!vehicles.Any()) return [];
               
        var ids = vehicles.Select(v => v.Id).ToList();

        var reservations = await _reservationRepo
            .GetActiveByVehicleIdsAsync(ids);

        var busyVehicleIds = reservations
            .Where(r => r.Overlaps(
                request.PickupDateTime,
                request.ReturnDateTime))
            .Select(r => r.VehicleId)
            .Distinct()
            .ToHashSet();

        var available = vehicles
            .Where(v => !busyVehicleIds.Contains(v.Id))
            .ToList();

        return available.Select(v => new VehicleResultDto
        {
            Id = v.Id,
            Plate = v.Plate,
            Model = v.Model,
            Brand = v.Brand,
            DailyPrice = v.DailyPrice
        }).ToList();
    }
}
