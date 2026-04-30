using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AllJob.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected async Task ValidateAsync<T>(
        IValidator<T> validator, T dto)
    {
        if (dto == null)
        {
            throw new Application.Exceptions.BadRequestException("The request body is empty or the data format is invalid.");
        }

      

        var result = await validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new Application.Exceptions.ValidationException(
                result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage)
                            .ToArray()));
    }
}