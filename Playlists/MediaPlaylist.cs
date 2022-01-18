using PlaylistParser.Models;
using PlaylistParser.Tags.Media;

namespace PlaylistParser.Playlists
{
    public class MediaPlaylist : BasePlaylist
    {
        public List<TagInfo> globalTags = new();
        public List<MediaSegment> mediaSegments = new();

        public XTargetDurationTag targetDurationTag;
        public XMediaSequenceTag? mediaSequenceTag;

        public bool endList = false;

        /* В процессе парсинга, если мы встречаем теги медиа сегментов, мы должны их хранить
         * А затем, когда находим ссылку, скрепляем это всё вместе и записываем. */
        private List<TagInfo> currentMediaSegmentsTags = new();

        public MediaPlaylist(string text) : base(text)
        {
            if (targetDurationTag == null)
            {
                throw new PlaylistException($"{nameof(MediaPlaylist)} {nameof(targetDurationTag)} == null");
            }
        }

        /*protected override void ContinueParsing(string text)
        {
            if (currentMediaSegmentsTags.Count > 0)
                throw new PlaylistException("MediaPlaylist ContinueParsing");
        }*/

        protected override void OnTagLine(string tag, string? attributes)
        {
            //зачем?
            //if (masterPlaylistTags.Contains(tag)) throw new PlaylistException($"Bad tag in media playlist\n{tag}:{value}");

            //можно было бы быть 5хедом и сделать словарик, но как бы ну как бы ну как бы ну впадлу да
            if (tag == "#EXT-X-TARGETDURATION")
            {
                targetDurationTag = new XTargetDurationTag(attributes!);
                return;
            }
            else if (tag == "#EXT-X-ENDLIST")
            {
                endList = true;
                return;
            }
            else if (tag == "#EXT-X-MEDIA-SEQUENCE")
            {
                mediaSequenceTag = new XMediaSequenceTag(attributes!);
                return;
            }

            var tagInfo = new TagInfo(tag, attributes);

            var list = mediaSegmentTags.Contains(tag) ? currentMediaSegmentsTags : globalTags;

            list.Add(tagInfo);
        }

        protected override void OnUriLine(string line, Uri uri)
        {
            if (currentMediaSegmentsTags.Count == 0) throw new PlaylistException($"Missing media segment tags\n{uri}");

            var media = new MediaSegment(uri, currentMediaSegmentsTags);
            mediaSegments.Add(media);

            currentMediaSegmentsTags = new List<TagInfo>();
        }
    }
}