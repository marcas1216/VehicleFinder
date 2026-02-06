using Vehicles.Domain.Entities;
using System;

public class ReservationCommandBuilder
{
    private Guid _id = Guid.NewGuid();
    private Guid _vehicleId = Guid.NewGuid();
    private Guid _customerId = Guid.NewGuid();
    private DateTime _pickupDate = DateTime.UtcNow;
    private DateTime _returnDate = DateTime.UtcNow.AddDays(1);
    private string _pickupLocationCode = "LOC1";
    private string _returnLocationCode = "LOC2";

    public ReservationCommandBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ReservationCommandBuilder WithVehicle(Guid vehicleId)
    {
        _vehicleId = vehicleId;
        return this;
    }

    public ReservationCommandBuilder WithCustomer(Guid customerId)
    {
        _customerId = customerId;
        return this;
    }

    public ReservationCommandBuilder WithDates(DateTime pickup, DateTime returnDate)
    {
        _pickupDate = pickup;
        _returnDate = returnDate;
        return this;
    }

    public ReservationCommandBuilder WithLocations(string pickup, string returnLocation)
    {
        _pickupLocationCode = pickup;
        _returnLocationCode = returnLocation;
        return this;
    }

    public Reservation Build()
    {
        return new Reservation(
            _id,
            _vehicleId,
            _pickupDate,
            _returnDate,
            _pickupLocationCode,
            _returnLocationCode
        );
    }
}
