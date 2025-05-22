using System.ComponentModel.DataAnnotations;

namespace NeighborTakeHome.Models
{
    public class ParkingStall
    {
        public required Guid id { get; set; }
        public required Guid location_id { get; set; }
        public required int length { get; set; }
        public required int width { get; set; }
        public required int price_in_cents { get; set; }
    }
}
