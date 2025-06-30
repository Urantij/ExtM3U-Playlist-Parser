using System.Globalization;

namespace ExtM3UPlaylistParser.Tags.Media;

/// <summary>
/// https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.2.1
/// </summary>
public class InfTag : BaseTag
{
    public readonly float Duration;
    public readonly string? Title;

    public InfTag(string value)
        : base(value)
    {
        int separatorIndex = value.IndexOf(',');

        if (separatorIndex == -1)
        {
            Duration = float.Parse(value);
            return;
        }

        Duration = float.Parse(value[..separatorIndex], CultureInfo.InvariantCulture);
        Title = value[(separatorIndex + 1)..];
    }
}