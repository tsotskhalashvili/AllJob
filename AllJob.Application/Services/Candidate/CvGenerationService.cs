using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces.Repositories.Candidate;
using AllJob.Application.Interfaces.Services.Candidate;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Settings;
using AllJob.Domain.Entities.Candidate;
using Microsoft.Extensions.Options;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AllJob.Application.Services.Candidate;

public class CvGenerationService(
    ICandidateRepository candidateRepository,
    IFileUploadService fileUploadService,
    IOptions<GeminiSettings> geminSettings) : ICvGenerationService
{
    private readonly GeminiSettings _settings = geminSettings.Value;

    public async Task<string> GenerateCvAsync(Guid userId)
    {
        var candidate = await candidateRepository.GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        if (string.IsNullOrEmpty(candidate.FirstName) ||
            string.IsNullOrEmpty(candidate.LastName))
            throw new BadRequestException("First and last name are required");

        if (!candidate.Skills.Any())
            throw new BadRequestException("At least one skill is required");

        var cvSummary = await GenerateCvTextAsync(candidate);

        byte[]? photoBytes = null;
        if (!string.IsNullOrEmpty(candidate.PhotoUrl))
        {
            try
            {
                using var httpClient = new HttpClient();
                photoBytes = await httpClient.GetByteArrayAsync(candidate.PhotoUrl);
            }
            catch
            {
                // Photo download failed → CV without photo ✅
            }
        }

        var pdfBytes = GeneratePdf(candidate, cvSummary, photoBytes);

        using var stream = new MemoryStream(pdfBytes);
        var fileName = $"cv_{candidate.FirstName}_{candidate.LastName}_{Guid.NewGuid()}.pdf";
        return await fileUploadService.UploadPdfAsync(stream, fileName);
    }

    private async Task<string> GenerateCvTextAsync(CandidateProfile candidate)
    {
        var skills = string.Join(", ", candidate.Skills.Select(s => s.Skill.Name));

        var experiences = candidate.Experiences.Any()
            ? string.Join("\n", candidate.Experiences.Select(e =>
                $"- {e.Position} at {e.CompanyName} ({e.StartDate:yyyy} - {(e.EndDate.HasValue ? e.EndDate.Value.ToString("yyyy") : "Present")})"))
            : "No experience listed";

        var educations = candidate.Educations.Any()
            ? string.Join("\n", candidate.Educations.Select(e =>
                $"- {e.Degree} at {e.InstitutionName} ({e.StartDate:yyyy} - {(e.EndDate.HasValue ? e.EndDate.Value.ToString("yyyy") : "Present")})"))
            : "No education listed";

        var prompt = $"""
            Generate a professional CV summary for:
            Name: {candidate.FirstName} {candidate.LastName}
            Bio: {candidate.Bio}
            Skills: {skills}
            Experience:
            {experiences}
            Education:
            {educations}
            
            Return a concise professional summary (max 200 words).
            Do not include any markdown or formatting.
            """;

        using var client = new HttpClient();
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_settings.Model}:generateContent?key={_settings.ApiKey}";

        var requestBody = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = prompt } } }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        return result
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString() ?? string.Empty;
    }

    private byte[] GeneratePdf(CandidateProfile candidate, string cvSummary, byte[]? photoBytes)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Content().Column(col =>
                {
                    // Header — Photo + Name
                    col.Item().Row(row =>
                    {
                        if (photoBytes != null)
                        {
                            row.ConstantItem(80).Height(80)
                                .Image(photoBytes).FitArea();
                        }

                        row.RelativeItem()
                            .PaddingLeft(photoBytes != null ? 10 : 0)
                            .Column(nameCol =>
                            {
                                nameCol.Item()
                                    .Text($"{candidate.FirstName} {candidate.LastName}")
                                    .FontSize(24).Bold();

                                if (!string.IsNullOrEmpty(candidate.Bio))
                                    nameCol.Item().PaddingTop(5)
                                        .Text(candidate.Bio)
                                        .FontColor(Colors.Grey.Medium);
                            });
                    });

                    // Summary
                    col.Item().PaddingTop(15).Text("Professional Summary")
                        .FontSize(14).Bold();
                    col.Item().Text(cvSummary);

                    // Skills
                    if (candidate.Skills.Any())
                    {
                        col.Item().PaddingTop(15).Text("Skills")
                            .FontSize(14).Bold();
                        col.Item().Text(string.Join(", ",
                            candidate.Skills.Select(s => s.Skill.Name)));
                    }

                    // Experience
                    if (candidate.Experiences.Any())
                    {
                        col.Item().PaddingTop(15).Text("Experience")
                            .FontSize(14).Bold();
                        foreach (var exp in candidate.Experiences)
                        {
                            col.Item().Text($"{exp.Position} at {exp.CompanyName}").Bold();
                            col.Item().Text(
                                $"{exp.StartDate:yyyy} - {(exp.EndDate.HasValue ? exp.EndDate.Value.ToString("yyyy") : "Present")}")
                                .FontColor(Colors.Grey.Medium);
                        }
                    }

                    // Education
                    if (candidate.Educations.Any())
                    {
                        col.Item().PaddingTop(15).Text("Education")
                            .FontSize(14).Bold();
                        foreach (var edu in candidate.Educations)
                        {
                            col.Item().Text($"{edu.Degree} - {edu.InstitutionName}").Bold();
                            col.Item().Text(
                                $"{edu.StartDate:yyyy} - {(edu.EndDate.HasValue ? edu.EndDate.Value.ToString("yyyy") : "Present")}")
                                .FontColor(Colors.Grey.Medium);
                        }
                    }
                });
            });
        });

        return document.GeneratePdf();
    }
}