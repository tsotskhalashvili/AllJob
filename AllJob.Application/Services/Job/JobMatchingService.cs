using AllJob.Application.DTOs.Job;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces.Repositories.Candidate;
using AllJob.Application.Interfaces.Repositories.Jobs;
using AllJob.Application.Interfaces.Services.Job;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Mappings;
using AllJob.Application.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AllJob.Application.Services.Job;

public class JobMatchingService(
    ICandidateRepository candidateRepository,
    IJobRepository jobRepository,
    ICacheService cacheService,
    IHttpClientFactory httpClientFactory,
    IOptions<GeminiSettings> geminiSettings) : IJobMatchingService
{
    private readonly GeminiSettings _settings = geminiSettings.Value;

    private async Task<string> CallGeminiAsync(string prompt)
    {
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
                        new { text = prompt }
                    }
                }
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

    public async Task<IReadOnlyList<JobResponseDto>> GetRecommendedJobsAsync(Guid userId)
    {
        var cacheKey = $"job-recommendations:{userId}";
        var cached = cacheService.Get<IReadOnlyList<JobResponseDto>>(cacheKey);
        if (cached is not null) return cached;

        var candidate = await candidateRepository.GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        var candidateSkills = candidate.Skills
            .Select(s => s.Skill.Name)
            .ToList();

        if (!candidateSkills.Any())
            return new List<JobResponseDto>();

        var jobs = await jobRepository.GetRecentJobsAsync(720);
        if (!jobs.Any())
            return new List<JobResponseDto>();

        var jobsList = jobs.Select(j => new
        {
            id = j.Id.ToString(),
            title = j.Title,
            skills = j.JobSkills.Select(js => js.Skill.Name).ToList()
        }).ToList();

        var prompt = $"""
            You are a job matching assistant.
            Candidate skills: {string.Join(", ", candidateSkills)}
            
            Available jobs:
            {JsonSerializer.Serialize(jobsList)}
            
            Return ONLY a JSON array of top 5 job IDs ordered by relevance.
            Return ONLY the JSON array, no explanation, no markdown.
            Example: ["id1","id2","id3"]
            If no matches found, return empty array: []
            """;

        var responseText = await CallGeminiAsync(prompt);

        List<string> jobIds;
        try
        {
            jobIds = JsonSerializer.Deserialize<List<string>>(responseText.Trim())
                ?? new List<string>();
        }
        catch
        {
            return new List<JobResponseDto>();
        }

        var result = new List<JobResponseDto>();
        foreach (var idStr in jobIds)
        {
            if (Guid.TryParse(idStr, out var jobId))
            {
                var job = jobs.FirstOrDefault(j => j.Id == jobId);
                if (job is not null)
                    result.Add(job.ToDto());
            }
        }

        cacheService.Set(cacheKey, result, TimeSpan.FromMinutes(30));
        return result;
    }

    public async Task<int> GetJobMatchScoreAsync(Guid userId, Guid jobId)
    {
        var candidate = await candidateRepository.GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        var job = await jobRepository.GetJobWithDetailsAsync(jobId)
            ?? throw new NotFoundException("Job", jobId);

        var candidateSkills = candidate.Skills
            .Select(s => s.Skill.Name)
            .ToList();

        var jobSkills = job.JobSkills
            .Select(js => js.Skill.Name)
            .ToList();

        var prompt = $"""
            You are a job matching assistant.
            Candidate skills: {string.Join(", ", candidateSkills)}
            Job title: {job.Title}
            Job skills required: {string.Join(", ", jobSkills)}
            
            Return ONLY a number between 0-100 representing match score.
            Return ONLY the number, no explanation, no markdown.
            Example: 85
            """;

        var responseText = await CallGeminiAsync(prompt);
        return int.TryParse(responseText.Trim(), out var score) ? score : 0;
    }
}