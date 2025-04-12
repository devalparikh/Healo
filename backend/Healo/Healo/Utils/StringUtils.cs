namespace Healo.Utils;

public static class StringUtils
{
    public static string AddSpacesToCamelCase(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;

        return string.Concat(text.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x.ToString() : x.ToString()));
    }

    public static string NormalizeString(string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        return text.Replace(" ", "").ToLowerInvariant();
    }
}
