using Vehicles.Domain.Entities;

public class VehicleBuilderIntegration
{
    private Guid _id = Guid.NewGuid();
    private string _plate = "INT123";
    private string _brand = "Mazda";
    private string _model = "CX5";
    private int _year = 2023;
    private string _vehicleTypeId = "SEDAN";
    private string _marketId = "CO";
    private string _pickupLocationCode = "LOC1";

    public VehicleBuilderIntegration WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public VehicleBuilderIntegration WithType(string type)
    {
        _vehicleTypeId = type;
        return this;
    }

    public VehicleBuilderIntegration WithLocation(string loc)
    {
        _pickupLocationCode = loc;
        return this;
    }

    public Vehicle Build()
        => new Vehicle(
            _id,
            _plate,
            _brand,
            _model,
            _year,
            _vehicleTypeId,
            _marketId,
            _pickupLocationCode
        );
}
