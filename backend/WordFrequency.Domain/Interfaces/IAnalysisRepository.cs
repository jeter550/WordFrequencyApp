namespace WordFrequency.Domain.Interfaces;

using Entities;

public interface IAnalysisRepository
{
    Task<WordFrequencyResult> AddAsync(WordFrequencyResult result);
    Task<WordFrequencyResult?> GetByIdAsync(Guid id);
    Task<List<WordFrequencyResult>> GetAllAsync();
}
