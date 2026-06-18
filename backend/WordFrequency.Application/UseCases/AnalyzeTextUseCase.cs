namespace WordFrequency.Application.UseCases;

using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using DTOs;
using Interfaces;

public class AnalyzeTextUseCase : IWordFrequencyService
{
    private readonly IWordFrequencyAnalyzer _analyzer;
    private readonly IAnalysisRepository _repository;

    public AnalyzeTextUseCase(IWordFrequencyAnalyzer analyzer, IAnalysisRepository repository)
    {
        _analyzer = analyzer;
        _repository = repository;
    }

    public async Task<AnalyzeTextResponse> AnalyzeTextAsync(string text)
    {
        var validationResult = TextInput.Create(text);

        return await validationResult.Match(
            onSuccess: async input => await ProcessValidTextAsync(input.Value),
            onFailure: error => throw new ArgumentException(error)
        );
    }

    private async Task<AnalyzeTextResponse> ProcessValidTextAsync(string text)
    {
        var wordCounts = _analyzer.Analyze(text);
        var results = wordCounts
            .Select((wc, index) => new WordCount { Word = wc.Key, Count = wc.Value, Rank = index + 1 })
            .ToList();

        var frequencyResult = new WordFrequencyResult
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            OriginalText = text,
            TotalWords = wordCounts.Sum(w => w.Value),
            UniqueWords = wordCounts.Count,
            Results = results
        };

        await _repository.AddAsync(frequencyResult);

        return MapToResponse(frequencyResult);
    }

    private static AnalyzeTextResponse MapToResponse(WordFrequencyResult result) =>
        new()
        {
            Id = result.Id,
            TotalWords = result.TotalWords,
            UniqueWords = result.UniqueWords,
            Results = result.Results
                .Select(r => new WordCountDto { Word = r.Word, Count = r.Count, Rank = r.Rank })
                .ToList()
        };
}
