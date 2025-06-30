using System.Globalization;

namespace ExtM3UPlaylistParser.Tags.Master;

public class XTwitchInfoTag : BaseAttributesTag
{
    public readonly float StreamTime;

    public XTwitchInfoTag(string value) : base(value)
    {
        StreamTime = float.Parse(RawAttributes["STREAM-TIME"], CultureInfo.InvariantCulture);
    }
}