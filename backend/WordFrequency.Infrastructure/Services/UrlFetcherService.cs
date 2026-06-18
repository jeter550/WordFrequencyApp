namespace WordFrequency.Infrastructure.Services;

using Application.Interfaces;

public class UrlFetcherService : IUrlFetcherService
{
    private readonly HttpClient _httpClient;
    private const int TimeoutSeconds = 30;

    public UrlFetcherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(TimeoutSeconds);
    }

    public async Task<string> FetchContentAsync(string url)
    {
        if (!IsValidUrl(url))
            throw new ArgumentException("Invalid URL format");

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Failed to fetch content from URL: {ex.Message}", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new InvalidOperationException($"Request timeout while fetching URL: {ex.Message}", ex);
        }
    }

    private static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
