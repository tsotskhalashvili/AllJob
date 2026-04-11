using AllJob.Application.Interfaces.Services.Subscription;
using Microsoft.AspNetCore.Mvc;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlanController(IPlanService planService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await planService.GetAllAsync();
        return Ok(result);
    }
}