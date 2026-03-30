using AllJob.Domain.Enums;

namespace AllJob.Application.DTOs.Job;

// class გამოვიყენეთ → nullable properties სჭირდება
public class JobFilterDto
{
    public string? Title { get; set; }
    public Guid? CategoryId { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public WorkType? WorkType { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
