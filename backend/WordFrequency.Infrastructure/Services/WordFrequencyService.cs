namespace WordFrequency.Infrastructure.Services;

using System.Text.RegularExpressions;
using Application.Interfaces;

public class WordFrequencyService : IWordFrequencyAnalyzer
{
    public Dictionary<string, int> Analyze(string text)
    {
        var normalized = NormalizeText(text);
        var words = ExtractWords(normalized);

        return words
            .GroupBy(w => w)
            .OrderByDescending(g => g.Count())
            .ToDictionary(g => g.Key, g => g.Count());
    }

    private static string NormalizeText(string text)
    {
        text = text.ToLowerInvariant();
        text = Regex.Replace(text, @"[^\w\s]", " ");
        return Regex.Replace(text, @"\s+", " ").Trim();
    }

    private static List<string> ExtractWords(string text)
    {
        return text
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(w => !string.IsNullOrWhiteSpace(w) && w.Length > 0)
            .ToList();
    }
}
