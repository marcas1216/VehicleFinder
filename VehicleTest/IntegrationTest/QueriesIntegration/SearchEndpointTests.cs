using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Vehicles.Application.Dto;
using VehicleTest.IntegrationTest.BuilderIntegration;
using VehicleTest.IntegrationTest.Factories;

public class SearchEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory factory;
    private readonly Guid pickupLocationId;
    private readonly Guid returnLocationId;

    public SearchEndpointTests(CustomWebApplicationFactory factory)
    {
        this.factory = factory;
                
        using var scope = factory.Services.CreateScope();
        var db = BuildCommandTest.GetDbContext(scope.ServiceProvider);

        var result = BuildCommandTest.SeedVehicles(db);
        pickupLocationId = result.pickupLocationId;
        returnLocationId = result.returnLocationId;
    }

    [Fact]
    public async Task Search_Should_Return_Vehicles_When_Available()
    {        
        var client = factory.CreateClient();

        var pickup = DateTime.UtcNow;
        var returnDate = DateTime.UtcNow.AddDays(1);

        var response = await client.GetAsync(
            $"/api/vehicles/search?pickupLocationId={pickupLocationId}&returnLocationId={returnLocationId}&pickup={pickup:o}&return={returnDate:o}"
        );

        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<List<VehicleResultDto>>();
                
        data!.Count.Should().BeGreaterThan(0);
    }
}
