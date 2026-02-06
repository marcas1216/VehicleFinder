namespace Vehicles.Domain.Entities;

public class Reservation
{
    public Guid Id { get; private set; }

    public Guid VehicleId { get; private set; }

    public DateTime PickupDateTime { get; private set; }
    public DateTime ReturnDateTime { get; private set; }

    public string PickupLocationCode { get; private set; } = default!;
    public string ReturnLocationCode { get; private set; } = default!;

    public ReservationStatus Status { get; private set; }

    private Reservation() { }

    public Reservation(
        Guid id,
        Guid vehicleId,
        DateTime pickupDateTime,
        DateTime returnDateTime,
        string pickupLocationCode,
        string returnLocationCode)
    {
        if (returnDateTime <= pickupDateTime)
            throw new ArgumentException("Return date must be greater than pickup date");

        Id = id;
        VehicleId = vehicleId;
        PickupDateTime = pickupDateTime;
        ReturnDateTime = returnDateTime;
        PickupLocationCode = pickupLocationCode;
        ReturnLocationCode = returnLocationCode;
        Status = ReservationStatus.Active;
    }

    public bool Overlaps(DateTime from, DateTime to)
    {
        return PickupDateTime < to && ReturnDateTime > from;
    }

    public void Cancel()
    {
        Status = ReservationStatus.Cancelled;
    }
}

public enum ReservationStatus
{
    Active = 1,
    Cancelled = 2,
    Completed = 3
}
