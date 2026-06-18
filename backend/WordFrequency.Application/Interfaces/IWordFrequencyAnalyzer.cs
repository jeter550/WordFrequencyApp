namespace WordFrequency.Application.Interfaces;

public interface IWordFrequencyAnalyzer
{
    Dictionary<string, int> Analyze(string text);
}
