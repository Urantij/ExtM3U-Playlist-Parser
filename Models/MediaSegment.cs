using ExtM3UPlaylistParser.Tags.Media;

namespace ExtM3UPlaylistParser.Models;

//https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.2
public class MediaSegment
{
    public readonly Uri Uri;

    //вообще логичнее тут было использовать массив, но смысла мало
    /// <summary>
    /// Содержит теги, которые не расфасовались по полям.
    /// </summary>
    public readonly List<TagInfo> UnAddedTags;

    /// <summary>
    /// Обязательный
    /// </summary>
    public readonly InfTag InfTag;

    public readonly XProgramDateTimeTag? ProgramDateTag;

    public MediaSegment(Uri uri, List<TagInfo> tags)
    {
        this.Uri = uri;
        this.UnAddedTags = tags;

        var extInfTagInfo = tags.Single(t => t.Tag == "#EXTINF");
        tags.Remove(extInfTagInfo);
        InfTag = new InfTag(extInfTagInfo.Value!);

        var programDateTagInfo = tags.SingleOrDefault(t => t.Tag == "#EXT-X-PROGRAM-DATE-TIME");
        if (programDateTagInfo != null)
        {
            tags.Remove(programDateTagInfo);
            ProgramDateTag = new XProgramDateTimeTag(programDateTagInfo.Value!);
        }
    }
}