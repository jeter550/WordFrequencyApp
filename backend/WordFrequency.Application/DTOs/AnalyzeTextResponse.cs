namespace WordFrequency.Application.DTOs;

public class AnalyzeTextResponse
{
    public Guid Id { get; set; }
    public int TotalWords { get; set; }
    public int UniqueWords { get; set; }
    public List<WordCountDto> Results { get; set; } = new();
}

public class WordCountDto
{
    public string Word { get; set; } = string.Empty;
    public int Count { get; set; }
    public int Rank { get; set; }
}
