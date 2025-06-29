using System.Globalization;

namespace ExtM3UPlaylistParser.Tags.Media;

/// <summary>
/// https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.3.1
/// </summary>
public class XTargetDurationTag : BaseTag
{
    public readonly int targetDuration;

    public XTargetDurationTag(string value) : base(value)
    {
        targetDuration = int.Parse(value, CultureInfo.InvariantCulture);
    }
}