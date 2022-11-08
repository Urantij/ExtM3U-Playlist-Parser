using ExtM3UPlaylistParser.Models;
using ExtM3UPlaylistParser.Playlists;
using ExtM3UPlaylistParser.Tags.Master;

namespace ExtM3UPlaylistParser.Parsers
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

            return new MasterPlaylist(globalTags, variantStreams, mediaDict);
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

                if (stream.streamInfTag.subtitles != null)
                {
                    stream.subtitlesMediaTags = mediaDict[stream.streamInfTag.subtitles];
                }

                if (stream.streamInfTag.closedCaptions != null)
                {
                    stream.closedCaptionsMediaTags = mediaDict[stream.streamInfTag.closedCaptions];
                }
            }
        }

        protected override void OnTagLine(string tag, string? value)
        {
            // зачем
            // if (mediaPlaylistTags.Contains(tag) || mediaSegmentTags.Contains(tag)) throw new PlaylistException($"Bad tag in master playlist\n{tag}:{attributes}");

            if (tag == "#EXT-X-STREAM-INF")
            {
                // Зачем?
                // if (lastStreamInfTag != null)
                // {
                //     throw new PlaylistException("Repeated EXT-X-STREAM-INF tag");
                // }

                lastStreamInfTag = new XStreamInfTag(value!);
                return;
            }
            else if (tag == "#EXT-X-MEDIA")
            {
                var mediaTag = new MediaTag(value!);

                if (!mediaDict.TryGetValue(mediaTag.groupId, out var list))
                {
                    list = new List<MediaTag>();
                    mediaDict.Add(mediaTag.groupId, list);
                }

                list.Add(mediaTag);
                return;
            }

            var tagInfo = new TagInfo(tag, value);
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