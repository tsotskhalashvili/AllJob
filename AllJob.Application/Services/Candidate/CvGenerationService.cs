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
    IHttpClientFactory httpClientFactory,
    IOptions<GeminiSettings> geminSettings) : ICvGenerationService
{
    private readonly GeminiSettings _settings = geminSettings.Value;

    public async Task<string> GenerateCvAsync(Guid userId, string lang)
    {
        var candidate = await candidateRepository.GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        if (string.IsNullOrEmpty(candidate.FirstName) ||
            string.IsNullOrEmpty(candidate.LastName))
            throw new BadRequestException("First and last name are required");

        //if (!candidate.Skills.Any())
        //    throw new BadRequestException("At least one skill is required");

        var cvSummary = await GenerateCvTextAsync(candidate, lang);

        byte[]? photoBytes = null;
        if (!string.IsNullOrEmpty(candidate.PhotoUrl) &&
            IsSafeCloudinaryUrl(candidate.PhotoUrl))
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient(); 
                photoBytes = await httpClient.GetByteArrayAsync(candidate.PhotoUrl);
            }
            catch { }
        }

        var pdfBytes = GeneratePdf(candidate, cvSummary, photoBytes);

        using var stream = new MemoryStream(pdfBytes);
        var fileName = $"cv_{candidate.FirstName}_{candidate.LastName}_{Guid.NewGuid()}.pdf";
        return await fileUploadService.UploadPdfAsync(stream, fileName);
    }

    private static bool IsSafeCloudinaryUrl(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return false;

        return uri.Scheme == "https" &&
               uri.Host.EndsWith("res.cloudinary.com");
    }

    private async Task<string> GenerateCvTextAsync(CandidateProfile candidate,string lang)
    {

        var skills = candidate.Skills != null && candidate.Skills.Any()
             ? string.Join(", ", candidate.Skills.Select(s => s.Skill.Name))
             : "Not specified";
        var profession = candidate.Experiences.FirstOrDefault()?.Position ?? "Professional";

        // განვსაზღვროთ ენა პრომპტისთვის
        string targetLanguage = lang.ToLower() == "en" ? "English" : "Georgian";

        var promptText = $"""
        Generate a professional CV summary for a {profession} named {candidate.FirstName} {candidate.LastName}.
        Skills: {skills}.
        Bio: {candidate.Bio}.
        
        Instruction: Write the summary strictly in {targetLanguage} language.
        Focus on industry-specific achievements. 
        Return only the summary text, no markdown, max 150 words.
        """;
        var client = httpClientFactory.CreateClient();

      
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_settings.Model}:generateContent?key={_settings.ApiKey}";

        var requestBody = new
        {
            contents = new[]
            {
            new
            {
                parts = new[]
                {
                    new { text = promptText }
                }
            }
        }
        };

        // მოთხოვნის გაგზავნა
        var response = await client.PostAsJsonAsync(url, requestBody);

        if (!response.IsSuccessStatusCode)
        {
            var errorDetail = await response.Content.ReadAsStringAsync();
            throw new Exception($"Gemini API Error ({response.StatusCode}): {errorDetail}");
        }

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();

        try
        {
            return result
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? string.Empty;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to parse Gemini response. " + ex.Message);
        }
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

                    col.Item().PaddingTop(15).Text("Professional Summary")
                        .FontSize(14).Bold();
                    col.Item().Text(cvSummary);

                    if (candidate.Skills.Any())
                    {
                        col.Item().PaddingTop(15).Text("Skills")
                            .FontSize(14).Bold();
                        col.Item().Text(string.Join(", ",
                            candidate.Skills.Select(s => s.Skill.Name)));
                    }

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