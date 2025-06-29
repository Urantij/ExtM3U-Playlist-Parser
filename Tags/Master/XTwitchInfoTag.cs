using System.Globalization;

namespace ExtM3UPlaylistParser.Tags.Master;

public class XTwitchInfoTag : BaseAttributesTag
{
    public readonly float streamTime;

    public XTwitchInfoTag(string value) : base(value)
    {
        streamTime = float.Parse(rawAttributes["STREAM-TIME"], CultureInfo.InvariantCulture);
    }
}