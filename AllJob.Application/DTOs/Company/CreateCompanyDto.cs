namespace AllJob.Application.DTOs.Company;

public record CreateCompanyDto(
    string Name,
    string LogoUrl,
    string Website,
    string Industry
);