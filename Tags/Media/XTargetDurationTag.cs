using System.Globalization;

namespace ExtM3UPlaylistParser.Tags.Media;

/// <summary>
/// https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.3.1
/// </summary>
public class XTargetDurationTag : BaseTag
{
    public readonly int TargetDuration;

    public XTargetDurationTag(string value) : base(value)
    {
        TargetDuration = int.Parse(value, CultureInfo.InvariantCulture);
    }
}