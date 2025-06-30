using ExtM3UPlaylistParser.Models;

namespace ExtM3UPlaylistParser.Tags.Master;

//https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.4.1
public class MediaTag : BaseAttributesTag
{
    public readonly string MediaType;
    public readonly Uri? Uri;
    public readonly string GroupId;
    public readonly string? Language;
    public readonly string? AssocLanguage;
    public readonly string Name;
    public readonly bool Default;
    public readonly bool AutoSelect;
    public readonly bool Forced;
    public readonly string? InstreamId;
    public readonly string? Characteristics;
    public readonly string? Channels;

    public MediaTag(string value) : base(value)
    {
        MediaType = RawAttributes["TYPE"];
        Models.MediaType.FindType(MediaType);

        if (TryGetQuotedStringAttribute("URI", out string? uriValue))
        {
            Uri = new Uri(uriValue!, UriKind.RelativeOrAbsolute);
        }

        GroupId = RawAttributes["GROUP-ID"];

        TryGetQuotedStringAttribute("LANGUAGE", out Language);
        TryGetQuotedStringAttribute("ASSOC-LANGUAGE", out AssocLanguage);

        Name = RawAttributes["NAME"];

        TryGetBoolAttribute("DEFAULT", out Default);
        TryGetBoolAttribute("AUTOSELECT", out AutoSelect);
        TryGetBoolAttribute("FORCED", out Forced);

        TryGetQuotedStringAttribute("INSTREAM-ID", out InstreamId);
        TryGetQuotedStringAttribute("CHARACTERISTICS", out Characteristics);
        TryGetQuotedStringAttribute("CHANNELS", out Channels);
    }
}