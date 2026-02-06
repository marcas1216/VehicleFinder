
using Vehicles.Domain.Interfaces;

namespace Vehicles.Infrastructure.Mongo
{
    public class FakeMarketCatalogService : IMarketCatalogService
    {
        public Task<List<string>> GetAllowedVehicleTypeIdsByLocation(Guid locationId)
        {
            return Task.FromResult(new List<string>
        {
            "SUV",
            "SEDAN",
            "PICKUP"
        });
        }
    }

}
