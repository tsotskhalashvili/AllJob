using AllJob.Application.DTOs.Common;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Shared;

namespace AllJob.Application.Services.Shared;

public class AddressService(
    IGenericRepository<Address> addressRepository,
    ICacheService cacheService) : IAddressService
{
    private const string CacheKey = "addresses:all";

    public async Task<IReadOnlyList<AddressResponseDto>> GetAllAsync()
    {
        var cached = cacheService.Get<IReadOnlyList<AddressResponseDto>>(CacheKey);
        if (cached is not null) return cached;

        var addresses = await addressRepository.GetAllAsync();
        var result = addresses.Select(a => a.ToDto()).ToList();

        cacheService.Set(CacheKey, result, TimeSpan.FromHours(24));
        return result;
    }
}