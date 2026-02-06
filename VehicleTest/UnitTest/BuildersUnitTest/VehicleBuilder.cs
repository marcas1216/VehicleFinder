using Vehicles.Domain.Entities;

public class VehicleBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _plate = "AAA111";
    private string _brand = "DefaultBrand";
    private string _model = "DefaultModel";
    private int _year = 2024;
    private string _vehicleTypeId = "SEDAN";
    private string _marketId = "CO";
    private string _pickupLocationCode = "LOC1";

    public VehicleBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public VehicleBuilder WithType(string type)
    {
        _vehicleTypeId = type;
        return this;
    }

    public VehicleBuilder WithLocation(string code)
    {
        _pickupLocationCode = code;
        return this;
    }

    public Vehicle Build()
    {
        return new Vehicle(
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
}
