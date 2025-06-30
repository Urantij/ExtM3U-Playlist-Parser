using ExtM3UPlaylistParser.Exceptions;
using ExtM3UPlaylistParser.Models;
using ExtM3UPlaylistParser.Playlists;
using ExtM3UPlaylistParser.Tags.Master;

namespace ExtM3UPlaylistParser.Parsers;

public class MasterParser : BaseParser<MasterPlaylist>
{
    private readonly List<TagInfo> _globalTags = new();
    private readonly List<VariantStream> _variantStreams = new();

    /// <summary>
    /// Group-Id к тегам с медиа
    /// </summary>
    private readonly Dictionary<string, List<MediaTag>> _mediaDict = new();

    private XStreamInfTag? _lastStreamInfTag = null;

    public override MasterPlaylist Parse(string text)
    {
        StartParsing(text);

        return new MasterPlaylist(_globalTags, _variantStreams, _mediaDict);
    }

    protected override void ContinueParsing()
    {
        foreach (var stream in _variantStreams)
        {
            if (stream.StreamInfTag.Audio != null)
            {
                stream.AudioMediaTags = _mediaDict[stream.StreamInfTag.Audio];
            }

            if (stream.StreamInfTag.Video != null)
            {
                stream.VideoMediaTags = _mediaDict[stream.StreamInfTag.Video];
            }

            if (stream.StreamInfTag.Subtitles != null)
            {
                stream.SubtitlesMediaTags = _mediaDict[stream.StreamInfTag.Subtitles];
            }

            if (stream.StreamInfTag.ClosedCaptions != null)
            {
                stream.ClosedCaptionsMediaTags = _mediaDict[stream.StreamInfTag.ClosedCaptions];
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

            _lastStreamInfTag = new XStreamInfTag(value!);
            return;
        }
        else if (tag == "#EXT-X-MEDIA")
        {
            var mediaTag = new MediaTag(value!);

            if (!_mediaDict.TryGetValue(mediaTag.GroupId, out var list))
            {
                list = new List<MediaTag>();
                _mediaDict.Add(mediaTag.GroupId, list);
            }

            list.Add(mediaTag);
            return;
        }

        var tagInfo = new TagInfo(tag, value);
        _globalTags.Add(tagInfo);
    }

    protected override void OnUriLine(string tag, Uri uri)
    {
        if (_lastStreamInfTag == null) throw new PlaylistException($"Missing #EXT-X-STREAM-INF tag\n{uri}");

        var variantStream = new VariantStream(uri, _lastStreamInfTag);
        _variantStreams.Add(variantStream);

        _lastStreamInfTag = null;
    }
}