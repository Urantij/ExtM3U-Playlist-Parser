using ExtM3UPlaylistParser.Tags.Media;

namespace ExtM3UPlaylistParser.Models
{
    //https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.2
    public class MediaSegment
    {
        public readonly Uri uri;

        //вообще логичнее тут было использовать массив, но смысла мало
        /// <summary>
        /// Содержит теги, которые не расфасовались по полям.
        /// </summary>
        public readonly List<TagInfo> unAddedTags;

        /// <summary>
        /// Обязательный
        /// </summary>
        public readonly InfTag infTag;
        public readonly XProgramDateTimeTag? programDateTag;

        public MediaSegment(Uri uri, List<TagInfo> tags)
        {
            this.uri = uri;
            this.unAddedTags = tags;

            var extInfTagInfo = tags.Single(t => t.tag == "#EXTINF");
            tags.Remove(extInfTagInfo);
            infTag = new InfTag(extInfTagInfo.value!);

            var programDateTagInfo = tags.SingleOrDefault(t => t.tag == "#EXT-X-PROGRAM-DATE-TIME");
            if (programDateTagInfo != null)
            {
                tags.Remove(programDateTagInfo);
                programDateTag = new XProgramDateTimeTag(programDateTagInfo.value!);
            }
        }
    }
}