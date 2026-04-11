using AllJob.Application.Interfaces.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController(
    IAddressService addressService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await addressService.GetAllAsync();
        return Ok(result);
    }
}