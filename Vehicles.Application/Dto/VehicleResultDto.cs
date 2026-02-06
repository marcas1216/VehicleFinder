
namespace Vehicles.Application.Dto
{
    public class VehicleResultDto
    {
        public Guid Id { get; set; }
        public string Plate { get; set; } = default!;
        public string Model { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public decimal? DailyPrice { get; set; }
    }
}
