namespace WordFrequency.Application.Interfaces;

public interface IUrlFetcherService
{
    Task<string> FetchContentAsync(string url);
}
