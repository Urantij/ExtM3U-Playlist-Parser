using ExtM3UPlaylistParser.Models;

namespace ExtM3UPlaylistParser.Tags.Master;

//https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.4.1
public class MediaTag : BaseAttributesTag
{
    public readonly string mediaType;
    public readonly Uri? uri;
    public readonly string groupId;
    public readonly string? language;
    public readonly string? assocLanguage;
    public readonly string name;
    public readonly bool @default;
    public readonly bool autoSelect;
    public readonly bool forced;
    public readonly string? instreamId;
    public readonly string? characteristics;
    public readonly string? channels;

    public MediaTag(string value) : base(value)
    {
        mediaType = rawAttributes["TYPE"];
        MediaType.FindType(mediaType);

        if (TryGetQuotedStringAttribute("URI", out string? uriValue))
        {
            uri = new Uri(uriValue!, UriKind.RelativeOrAbsolute);
        }

        groupId = rawAttributes["GROUP-ID"];

        TryGetQuotedStringAttribute("LANGUAGE", out language);
        TryGetQuotedStringAttribute("ASSOC-LANGUAGE", out assocLanguage);

        name = rawAttributes["NAME"];

        TryGetBoolAttribute("DEFAULT", out @default);
        TryGetBoolAttribute("AUTOSELECT", out autoSelect);
        TryGetBoolAttribute("FORCED", out forced);

        TryGetQuotedStringAttribute("INSTREAM-ID", out instreamId);
        TryGetQuotedStringAttribute("CHARACTERISTICS", out characteristics);
        TryGetQuotedStringAttribute("CHANNELS", out channels);
    }
}