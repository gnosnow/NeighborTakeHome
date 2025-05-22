namespace NeighborTakeHome.Models
{
    public class CheapestPathsReturn
    {
        public Guid location_id { get; set; }
        public List<Guid> listing_ids { get; set; }
        public int total_price_in_cents  { get; set; }
    }
}
