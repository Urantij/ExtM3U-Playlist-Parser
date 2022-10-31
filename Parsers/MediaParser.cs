using PlaylistParser.Models;
using PlaylistParser.Playlists;
using PlaylistParser.Tags.Media;

namespace PlaylistParser.Parsers
{
    public class MediaParser : BaseParser<MediaPlaylist>
    {
        /* В процессе парсинга, если мы встречаем теги медиа сегментов, мы должны их хранить.
         * А затем, когда находим ссылку, скрепляем это всё вместе и записываем. */
        private List<TagInfo> currentMediaSegmentsTags = new();

        private readonly List<TagInfo> globalTags = new();
        private readonly List<MediaSegment> mediaSegments = new();

        private XTargetDurationTag? targetDurationTag;
        private XMediaSequenceTag? mediaSequenceTag;
        private bool endList = false;

        public override MediaPlaylist Parse(string text)
        {
            StartParsing(text);

            // The EXT-X-TARGETDURATION tag is REQUIRED
            if (targetDurationTag == null)
            {
                throw new PlaylistException($"{nameof(MediaPlaylist)} {nameof(targetDurationTag)}");
            }

            return new MediaPlaylist(globalTags, mediaSegments, targetDurationTag, mediaSequenceTag, endList);
        }

        /*protected override void ContinueParsing(string text)
        {
            if (currentMediaSegmentsTags.Count > 0)
                throw new PlaylistException("MediaPlaylist ContinueParsing");
        }*/

        protected override void OnTagLine(string tag, string? value)
        {
            // зачем?
            // if (masterPlaylistTags.Contains(tag)) throw new PlaylistException($"Bad tag in media playlist\n{tag}:{value}");

            // можно было бы быть 5хедом и сделать словарик, но как бы ну как бы ну как бы ну впадлу да
            if (tag == "#EXT-X-TARGETDURATION")
            {
                targetDurationTag = new XTargetDurationTag(value!);
                return;
            }
            else if (tag == "#EXT-X-ENDLIST")
            {
                endList = true;
                return;
            }
            else if (tag == "#EXT-X-MEDIA-SEQUENCE")
            {
                mediaSequenceTag = new XMediaSequenceTag(value!);
                return;
            }

            var tagInfo = new TagInfo(tag, value);

            var list = mediaSegmentTags.Contains(tag) ? currentMediaSegmentsTags : globalTags;

            list.Add(tagInfo);
        }

        protected override void OnUriLine(string tag, Uri uri)
        {
            if (currentMediaSegmentsTags.Count == 0) throw new PlaylistException($"Missing media segment tags\n{uri}");

            var media = new MediaSegment(uri, currentMediaSegmentsTags);
            mediaSegments.Add(media);

            currentMediaSegmentsTags = new List<TagInfo>();
        }
    }
}