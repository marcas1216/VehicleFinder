using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vehicles.Application.Queries;

[ApiController]
[Route("api/vehicles")]
public class VehiclesReadController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehiclesReadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        Guid pickupLocationId,
        Guid returnLocationId,
        DateTime pickup,
        DateTime @return)
    {
        var result = await _mediator.Send(
            new SearchVehiclesQuery(
                pickupLocationId,
                returnLocationId,
                pickup,
                @return));

        return Ok(result);
    }
}
