namespace WordFrequency.Infrastructure.Services;

using System.Text.RegularExpressions;
using Application.Interfaces;

public class HtmlParserService : IHtmlParserService
{
    public string ExtractText(string html)
    {
        var text = RemoveScripts(html);
        text = RemoveStyles(text);
        text = DecodeHtmlEntities(text);
        text = RemoveHtmlTags(text);
        text = CleanWhitespace(text);

        return text.Trim();
    }

    private static string RemoveScripts(string html)
    {
        return Regex.Replace(html, @"<script[^>]*>[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
    }

    private static string RemoveStyles(string html)
    {
        return Regex.Replace(html, @"<style[^>]*>[\s\S]*?</style>", "", RegexOptions.IgnoreCase);
    }

    private static string DecodeHtmlEntities(string text)
    {
        var entities = new Dictionary<string, string>
        {
            { "&nbsp;", " " },
            { "&amp;", "&" },
            { "&lt;", "<" },
            { "&gt;", ">" },
            { "&quot;", "\"" },
            { "&#39;", "'" },
            { "&apos;", "'" }
        };

        foreach (var entity in entities)
        {
            text = text.Replace(entity.Key, entity.Value, StringComparison.OrdinalIgnoreCase);
        }

        text = Regex.Replace(text, @"&#(\d+);", m =>
        {
            if (int.TryParse(m.Groups[1].Value, out int charCode))
                return ((char)charCode).ToString();
            return m.Value;
        });

        return text;
    }

    private static string RemoveHtmlTags(string text)
    {
        return Regex.Replace(text, @"<[^>]+>", " ");
    }

    private static string CleanWhitespace(string text)
    {
        text = Regex.Replace(text, @"\s+", " ");
        return text.Trim();
    }
}
