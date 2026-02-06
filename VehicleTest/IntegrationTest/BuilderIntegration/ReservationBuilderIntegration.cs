using Vehicles.Domain.Entities;

public class ReservationBuilderIntegration
{
    private Guid _id = Guid.NewGuid();
    private Guid _vehicleId = Guid.NewGuid();
    private DateTime _pickup = DateTime.UtcNow.AddDays(1);
    private DateTime _return = DateTime.UtcNow.AddDays(3);
    private string _pickupLoc = "LOC1";
    private string _returnLoc = "LOC1";

    public ReservationBuilderIntegration ForVehicle(Guid vehicleId)
    {
        _vehicleId = vehicleId;
        return this;
    }

    public ReservationBuilderIntegration WithDates(DateTime pickup, DateTime ret)
    {
        _pickup = pickup;
        _return = ret;
        return this;
    }

    public Reservation Build()
        => new Reservation(
            _id,
            _vehicleId,
            _pickup,
            _return,
            _pickupLoc,
            _returnLoc
        );
}
