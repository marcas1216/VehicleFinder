using MediatR;
using Vehicles.Application.Dto;

namespace Vehicles.Application.Queries;

public record SearchVehiclesQuery(
    Guid PickupLocationId,
    Guid ReturnLocationId,
    DateTime PickupDateTime,
    DateTime ReturnDateTime
) : IRequest<List<VehicleResultDto>>;
