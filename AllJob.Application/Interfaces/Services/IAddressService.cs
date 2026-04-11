using AllJob.Application.DTOs.Common;

namespace AllJob.Application.Interfaces.Services;

public interface IAddressService
{
    Task<IReadOnlyList<AddressResponseDto>> GetAllAsync();
}
