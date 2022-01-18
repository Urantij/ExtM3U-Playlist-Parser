using PlaylistParser.Models;
using PlaylistParser.Playlists;
using PlaylistParser.Tags.Master;

namespace PlaylistParser.Parsers
{
    public class MasterParser : BaseParser<MasterPlaylist>
    {
        private readonly List<TagInfo> globalTags = new();
        private readonly List<VariantStream> variantStreams = new();
        /// <summary>
        /// Group-Id к тегам с медиа
        /// </summary>
        private readonly Dictionary<string, List<MediaTag>> mediaDict = new();

        private XStreamInfTag? lastStreamInfTag = null;

        public override MasterPlaylist Parse(string text)
        {
            StartParsing(text);

            MasterPlaylist result = new(globalTags, variantStreams, mediaDict);

            return result;
        }

        protected override void ContinueParsing()
        {
            foreach (var stream in variantStreams)
            {
                if (stream.streamInfTag.audio != null)
                {
                    stream.audioMediaTags = mediaDict[stream.streamInfTag.audio];
                }

                if (stream.streamInfTag.video != null)
                {
                    stream.videoMediaTags = mediaDict[stream.streamInfTag.video];
                }

                //TODO? также нужно сделать с SUBTITLES и CLOSED-CAPTIONS.
                //Вообще, это же типа медиап, как-то можно через словарь сделать.
                //но бля ну похуй ну да..
            }
        }

        protected override void OnTagLine(string tag, string? attributesValue)
        {
            //зачем
            //if (mediaPlaylistTags.Contains(tag) || mediaSegmentTags.Contains(tag)) throw new PlaylistException($"Bad tag in master playlist\n{tag}:{attributes}");

            if (tag == "#EXT-X-STREAM-INF")
            {
                /* Зачем?
                if (lastStreamInfTag != null)
                {
                    throw new PlaylistException("Repeated EXT-X-STREAM-INF tag");
                }*/

                lastStreamInfTag = new XStreamInfTag(attributesValue!);
                return;
            }
            else if (tag == "#EXT-X-MEDIA")
            {
                var mediaTag = new MediaTag(attributesValue!);

                if (!mediaDict.TryGetValue(mediaTag.groupId, out var list))
                {
                    list = new List<MediaTag>();
                    mediaDict.Add(mediaTag.groupId, list);
                }

                list.Add(mediaTag);
                return;
            }

            var tagInfo = new TagInfo(tag, attributesValue);
            globalTags.Add(tagInfo);
        }

        protected override void OnUriLine(string tag, Uri uri)
        {
            if (lastStreamInfTag == null) throw new PlaylistException($"Missing #EXT-X-STREAM-INF tag\n{uri}");

            var variantStream = new VariantStream(uri, lastStreamInfTag);
            variantStreams.Add(variantStream);

            lastStreamInfTag = null;
        }
    }
}