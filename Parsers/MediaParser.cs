using ExtM3UPlaylistParser.Exceptions;
using ExtM3UPlaylistParser.Models;
using ExtM3UPlaylistParser.Playlists;
using ExtM3UPlaylistParser.Tags.Media;

namespace ExtM3UPlaylistParser.Parsers;

public class MediaParser : BaseParser<MediaPlaylist>
{
    /* В процессе парсинга, если мы встречаем теги медиа сегментов, мы должны их хранить.
     * А затем, когда находим ссылку, скрепляем это всё вместе и записываем. */
    private List<TagInfo> _currentMediaSegmentsTags = new();

    private readonly List<TagInfo> _globalTags = new();
    private readonly List<MediaSegment> _mediaSegments = new();

    private XTargetDurationTag? _targetDurationTag;
    private XMediaSequenceTag? _mediaSequenceTag;
    private bool _endList = false;

    public override MediaPlaylist Parse(string text)
    {
        StartParsing(text);

        // The EXT-X-TARGETDURATION tag is REQUIRED
        if (_targetDurationTag == null)
        {
            throw new PlaylistException($"{nameof(MediaPlaylist)} {nameof(_targetDurationTag)}");
        }

        return new MediaPlaylist(_globalTags, _mediaSegments, _targetDurationTag, _mediaSequenceTag, _endList);
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
            _targetDurationTag = new XTargetDurationTag(value!);
            return;
        }
        else if (tag == "#EXT-X-ENDLIST")
        {
            _endList = true;
            return;
        }
        else if (tag == "#EXT-X-MEDIA-SEQUENCE")
        {
            _mediaSequenceTag = new XMediaSequenceTag(value!);
            return;
        }

        var tagInfo = new TagInfo(tag, value);

        var list = MediaSegmentTags.Contains(tag) ? _currentMediaSegmentsTags : _globalTags;

        list.Add(tagInfo);
    }

    protected override void OnUriLine(string tag, Uri uri)
    {
        if (_currentMediaSegmentsTags.Count == 0) throw new PlaylistException($"Missing media segment tags\n{uri}");

        var media = new MediaSegment(uri, _currentMediaSegmentsTags);
        _mediaSegments.Add(media);

        _currentMediaSegmentsTags = new List<TagInfo>();
    }
}