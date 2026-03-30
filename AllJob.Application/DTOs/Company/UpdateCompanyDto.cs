namespace AllJob.Application.DTOs.Company;

public record UpdateCompanyDto(
    string Name,
    string LogoUrl,
    string Website,
    string Industry
);