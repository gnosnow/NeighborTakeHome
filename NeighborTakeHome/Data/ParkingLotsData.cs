using NeighborTakeHome.Models;
using System.Text.Json;

namespace NeighborTakeHome.Data
{
    public class ParkingLotsData
    {
        public Dictionary<Guid, List<ParkingStall>> parkingstalls { get; }
        public ParkingLotsData()
        {
            var json = File.ReadAllText(Path.Combine("Data", "listings.json"));
            var stalls = JsonSerializer.Deserialize<List<ParkingStall>>(json) ?? new List<ParkingStall>();
            parkingstalls = new Dictionary<Guid, List<ParkingStall>>();
            foreach (var stall in stalls)
            {
                if (parkingstalls.ContainsKey(stall.location_id) && !parkingstalls[stall.location_id].Contains(stall)) parkingstalls[stall.location_id].Add(stall);
                else parkingstalls.Add(stall.location_id, new List<ParkingStall>() { stall });
            }

            foreach (var parkingStall in parkingstalls)
            {
                parkingstalls[parkingStall.Key] = parkingstalls[parkingStall.Key].OrderBy(x => x.price_in_cents).ToList();
            }

        }

    }
}
