namespace WordFrequency.Application.Interfaces;

using DTOs;

public interface IWordFrequencyService
{
    Task<AnalyzeTextResponse> AnalyzeTextAsync(string text);
}
