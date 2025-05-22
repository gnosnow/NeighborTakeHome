using NeighborTakeHome.Models;
using System.Diagnostics;

namespace NeighborTakeHome.Operations
{
    public static class ParkingOperations
    {
        /// <summary>
        /// This will find teh cheapest set of stall for each location.
        /// NOTE: THIS ASSUMES YOU CAN ONLY PUT 1 CAR IN EACH STALL EVEN
        ///       I.E. IF A STALL IS LENGTH 20 AND WE HAVE 2 LENGTH 10 CARS.
        ///       This assumption was made as it seems hard to get cars out if
        ///       they are back to back to back, and depending on if people are storing them
        ///       themselves and it is not some valet service you wouldn't have access
        ///       to move the other cars.
        /// </summary>
        /// <param name="parkingLocations">The list of parking locations to use (we are assuming that it is ordered by cheapest)</param>
        /// <param name="parkingRequests"></param>
        /// <returns></returns>
        public static List<CheapestPathsReturn> FindCheapestStalls(Dictionary<Guid, List<ParkingStall>> parkingLocations, List<ParkingRequest> parkingRequests)
        {
            //order them to make it easy to perform a greedy algorithm with as we know we are using the space more efficiently based on my assumption above
            //that only one care will be in each stall
            var orderedParking = parkingRequests.OrderByDescending(x => x.length).ToList();
            
            List<CheapestPathsReturn> cheapestPaths = new List<CheapestPathsReturn>();

            //Loop through each parking Location to see what cars can fit
            foreach (var location in parkingLocations)
            {
                //save stalls for later use in the returned object
                List<ParkingStall> usedStalls = new List<ParkingStall>();
                List<ParkingStall> availableStalls = new List<ParkingStall>(location.Value);

                int totalCost = 0;
                bool allCarsFit = true;

                //loop through each car ordered by size so we can put the car in the biggest spot it can go to
                foreach (var parkingCar in orderedParking)
                {
                    //number of cars we need to fit
                    int numCarsToStore = parkingCar.quantity;

                    for (int i = 0; i < availableStalls.Count && numCarsToStore > 0; i++)
                    {
                        var stall = availableStalls[i];
                        //leaving check hear for.width even though we are assuming it is always 10
                        if (stall.length >= parkingCar.length && stall.width >= parkingCar.width)
                        {
                            usedStalls.Add(stall);
                            totalCost += stall.price_in_cents;
                            availableStalls.RemoveAt(i);
                            i--; // list shifted after removal
                            numCarsToStore--;
                        }
                    }

                    if (numCarsToStore > 0)
                    {
                        allCarsFit = false;
                        break;
                    }
                }

                //add the result to the cheapest paths if all cars fit
                if (allCarsFit)
                {
                    cheapestPaths.Add(new CheapestPathsReturn
                    {
                        location_id = location.Key,
                        listing_ids = usedStalls.Select(s => s.id).ToList(),
                        total_price_in_cents = totalCost
                    });
                }
            }
            // Sort by total price cheapest first
            cheapestPaths.Sort((a, b) => a.total_price_in_cents.CompareTo(b.total_price_in_cents));
            return cheapestPaths;
        }
    }
}
