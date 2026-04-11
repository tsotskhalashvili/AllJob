using AllJob.Application.DTOs.Common;
using AllJob.Domain.Entities.Shared;

namespace AllJob.Application.Mappings;

public static class AddressMappings
{
    public static AddressResponseDto ToDto(this Address address)
        => new(
            Id: address.Id,
            Country:address.Country,
            City:address.City
            );

}
