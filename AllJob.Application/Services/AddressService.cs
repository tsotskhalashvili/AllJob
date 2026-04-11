using AllJob.Application.DTOs.Common;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Shared;

namespace AllJob.Application.Services;

public class AddressService(
    IGenericRepository<Address> addressRepository)  : IAddressService
{
    public async Task<IReadOnlyList<AddressResponseDto>> GetAllAsync()
    {
        var adrresses = await addressRepository.GetAllAsync();
        return adrresses.Select(a => a.ToDto()).ToList();
    }
}
