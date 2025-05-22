using Microsoft.AspNetCore.Mvc;
using NeighborTakeHome.Data;

[ApiController]
[Route("api/[controller]")]
public class HealthCheckController : ControllerBase
{
    private readonly ParkingLotsData _dataService;
    public HealthCheckController(ParkingLotsData dataService)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public IActionResult Get() => Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
}