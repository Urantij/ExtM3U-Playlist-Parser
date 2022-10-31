using System.Globalization;

namespace PlaylistParser.Tags.Master
{
    //https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.4.2
    public class XStreamInfTag : BaseAttributesTag
    {
        public readonly int bandwidth;
        public readonly int? averageBandwidth;
        public readonly string? codecs;
        public readonly string? resolution;
        public readonly float? frameRate;
        public readonly string? hdcpLevel;
        public readonly string? audio;
        public readonly string? video;
        public readonly string? subtitles;
        public readonly string? closedCaptions;

        public XStreamInfTag(string value) : base(value)
        {
            bandwidth = int.Parse(rawAttributes["BANDWIDTH"], CultureInfo.InvariantCulture);
            TryGetIntAttribute("AVERAGE-BANDWIDTH", out averageBandwidth);
            TryGetQuotedStringAttribute("CODECS", out codecs);
            rawAttributes.TryGetValue("RESOLUTION", out resolution);
            TryGetFloatAttribute("FRAME-RATE", out frameRate);
            rawAttributes.TryGetValue("HDCP-LEVEL", out hdcpLevel);
            TryGetQuotedStringAttribute("AUDIO", out audio);
            TryGetQuotedStringAttribute("VIDEO", out video);
            TryGetQuotedStringAttribute("SUBTITLES", out subtitles);
            TryGetQuotedStringAttribute("CLOSED-CAPTIONS", out closedCaptions);
        }
    }
}