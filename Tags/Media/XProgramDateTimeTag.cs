namespace ExtM3UPlaylistParser.Tags.Media;

/// <summary>
/// https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.2.6
/// </summary>
public class XProgramDateTimeTag : BaseTag
{
    public readonly DateTimeOffset Time;

    public XProgramDateTimeTag(string value) : base(value)
    {
        Time = DateTimeOffset.Parse(value);
    }
}