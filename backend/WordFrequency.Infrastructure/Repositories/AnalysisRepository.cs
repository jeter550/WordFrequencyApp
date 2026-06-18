namespace WordFrequency.Infrastructure.Repositories;

using Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AnalysisRepository : IAnalysisRepository
{
    private readonly AppDbContext _context;

    public AnalysisRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<WordFrequencyResult> AddAsync(WordFrequencyResult result)
    {
        _context.Analyses.Add(result);
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<WordFrequencyResult?> GetByIdAsync(Guid id)
    {
        return await _context.Analyses.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<WordFrequencyResult>> GetAllAsync()
    {
        return await _context.Analyses.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }
}
