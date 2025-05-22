using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeighborTakeHome.Data;
using NeighborTakeHome.Models;
using NeighborTakeHome.Operations;

namespace NeighborTakeHome.Controllers
{
    [Route("")]
    [ApiController]
    public class GetParkingLocationsController : ControllerBase
    {
        private readonly ParkingLotsData _dataService;
        public GetParkingLocationsController(ParkingLotsData dataService)
        {
            _dataService = dataService;
        }

        [HttpPost]
        public IActionResult PostMessage([FromBody] List<ParkingRequest> message)
        {
            if (message.Count == 0)
            {
                return BadRequest("Sender and Content are required.");
            }

            var cheapestPaths = ParkingOperations.FindCheapestStalls(_dataService.parkingstalls, message);

            return Ok(cheapestPaths);
        }
    }
}
