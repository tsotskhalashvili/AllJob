using AllJob.Application.DTOs.Common;

namespace AllJob.Application.Interfaces.Services.Shared;

public interface IAddressService
{
    Task<IReadOnlyList<AddressResponseDto>> GetAllAsync();
}
