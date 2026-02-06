using Vehicles.Domain.Entities;

public class ReservationBuilder
{
    private Guid _id = Guid.NewGuid();
    private Guid _vehicleId = Guid.NewGuid();
    private DateTime _pickup = DateTime.UtcNow.AddDays(1);
    private DateTime _return = DateTime.UtcNow.AddDays(3);
    private string _pickupLocation = "LOC1";
    private string _returnLocation = "LOC1";

    public ReservationBuilder WithVehicle(Guid vehicleId)
    {
        _vehicleId = vehicleId;
        return this;
    }

    public ReservationBuilder WithDates(DateTime pickup, DateTime ret)
    {
        _pickup = pickup;
        _return = ret;
        return this;
    }

    public ReservationBuilder WithLocations(string pickup, string ret)
    {
        _pickupLocation = pickup;
        _returnLocation = ret;
        return this;
    }

    public Reservation Build()
    {
        return new Reservation(
            _id,
            _vehicleId,
            _pickup,
            _return,
            _pickupLocation,
            _returnLocation
        );
    }
}
