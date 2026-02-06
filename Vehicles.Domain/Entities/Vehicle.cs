namespace Vehicles.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; private set; }

    public string Plate { get; private set; } = default!;
    public string Brand { get; private set; } = default!;
    public string Model { get; private set; } = default!;
    public int Year { get; private set; }

    
    public string VehicleTypeId { get; private set; } = default!;
    public string MarketId { get; private set; } = default!;

    public string PickupLocationCode { get; private set; } = default!;

    public VehicleStatus Status { get; private set; }
    public decimal? DailyPrice { get; set; }
    public Guid LocationId { get; set; }

    private Vehicle() { } 

    public Vehicle(
        Guid id,
        string plate,
        string brand,
        string model,
        int year,
        string vehicleTypeId,
        string marketId,
        string pickupLocationCode)
    {
        Id = id;
        Plate = plate;
        Brand = brand;
        Model = model;
        Year = year;
        VehicleTypeId = vehicleTypeId;
        MarketId = marketId;
        PickupLocationCode = pickupLocationCode;
        Status = VehicleStatus.Available;
    }

    public void SetStatus(VehicleStatus status)
    {
        Status = status;
    }
}

public enum VehicleStatus
{
    Available = 1,
    Reserved = 2,
    OutOfService = 3
}
