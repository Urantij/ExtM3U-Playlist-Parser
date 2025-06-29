using System.Globalization;
using System.Text.RegularExpressions;
using ExtM3UPlaylistParser.Models;

namespace ExtM3UPlaylistParser.Tags;

/// <summary>
/// Описывает вид тегов с атрибутами. 
/// Вообще, это отдельный тип, потому что меня заебало разбираться с тем, что атрибуты нул или не нул
/// </summary>
public class BaseAttributesTag : BaseTag
{
    //https://datatracker.ietf.org/doc/html/rfc8216#section-4.2
    private static readonly Regex attributesRegex =
        new(@"(?<Name>[A-Z0-9\-]+)=((""(?<Value>.*?)"")|(?<Value>.*?))($|[,;])", RegexOptions.Compiled);

    public Dictionary<string, string> rawAttributes;

    public BaseAttributesTag(string value) : base(value)
    {
        var matches = attributesRegex.Matches(value);

        rawAttributes = matches.ToDictionary(key => key.Groups["Name"].Value,
            value => value.Groups["Value"].Value);
    }

    protected bool TryGetResolutionAttribute(string key, out Resolution? value)
    {
        if (rawAttributes.TryGetValue(key, out string? stringValue))
        {
            value = Resolution.Parse(stringValue);
            return true;
        }

        value = null;
        return false;
    }

    protected bool TryGetQuotedStringAttribute(string key, out string? value)
    {
        if (rawAttributes.TryGetValue(key, out value))
        {
            return true;
        }

        value = null;
        return false;
    }

    protected bool TryGetBoolAttribute(string key, out bool value, bool @default = false)
    {
        if (rawAttributes.TryGetValue(key, out string? stringValue))
        {
            value = stringValue == "YES";

            return true;
        }

        value = @default;
        return false;
    }

    protected bool TryGetIntAttribute(string key, out int? value)
    {
        if (rawAttributes.TryGetValue(key, out string? stringValue))
        {
            value = int.Parse(stringValue, CultureInfo.InvariantCulture);
            return true;
        }

        value = default;
        return false;
    }

    protected bool TryGetFloatAttribute(string key, out float? value)
    {
        if (rawAttributes.TryGetValue(key, out string? stringValue))
        {
            value = float.Parse(stringValue, CultureInfo.InvariantCulture);
            return true;
        }

        value = default;
        return false;
    }
}