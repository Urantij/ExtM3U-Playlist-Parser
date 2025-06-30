using System.Globalization;
using ExtM3UPlaylistParser.Models;

namespace ExtM3UPlaylistParser.Tags.Master;

//https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.4.2
public class XStreamInfTag : BaseAttributesTag
{
    public readonly int Bandwidth;
    public readonly int? AverageBandwidth;
    public readonly string? Codecs;
    public readonly Resolution? Resolution;
    public readonly float? FrameRate;
    public readonly string? HdcpLevel;
    public readonly string? Audio;
    public readonly string? Video;
    public readonly string? Subtitles;
    public readonly string? ClosedCaptions;

    public XStreamInfTag(string value) : base(value)
    {
        Bandwidth = int.Parse(RawAttributes["BANDWIDTH"], CultureInfo.InvariantCulture);
        TryGetIntAttribute("AVERAGE-BANDWIDTH", out AverageBandwidth);
        TryGetQuotedStringAttribute("CODECS", out Codecs);
        TryGetResolutionAttribute("RESOLUTION", out Resolution);
        TryGetFloatAttribute("FRAME-RATE", out FrameRate);
        RawAttributes.TryGetValue("HDCP-LEVEL", out HdcpLevel);
        TryGetQuotedStringAttribute("AUDIO", out Audio);
        TryGetQuotedStringAttribute("VIDEO", out Video);
        TryGetQuotedStringAttribute("SUBTITLES", out Subtitles);
        TryGetQuotedStringAttribute("CLOSED-CAPTIONS", out ClosedCaptions);
    }
}