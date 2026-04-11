namespace AllJob.Application.DTOs.Company;

public class CompanyFilterDto
{
    public string? Name { get; set; }
    public string? Industry { get; set; }
    public bool? IsVerified { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}