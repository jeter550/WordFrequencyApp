namespace WordFrequency.Domain.Entities;

public class WordFrequencyResult
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string OriginalText { get; set; } = string.Empty;
    public int TotalWords { get; set; }
    public int UniqueWords { get; set; }
    public List<WordCount> Results { get; set; } = new();
}

public class WordCount
{
    public string Word { get; set; } = string.Empty;
    public int Count { get; set; }
    public int Rank { get; set; }
}
