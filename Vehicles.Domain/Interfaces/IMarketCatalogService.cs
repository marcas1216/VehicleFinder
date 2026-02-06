namespace Vehicles.Domain.Interfaces;

public interface IMarketCatalogService
{
    Task<List<string>> GetAllowedVehicleTypeIdsByLocation(Guid locationId);
}
