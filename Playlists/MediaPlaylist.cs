using ExtM3UPlaylistParser.Models;
using ExtM3UPlaylistParser.Tags.Media;

namespace ExtM3UPlaylistParser.Playlists;

public class MediaPlaylist
{
    public readonly List<TagInfo> GlobalTags = new();
    public readonly List<MediaSegment> MediaSegments = new();

    public readonly XTargetDurationTag TargetDurationTag;
    public readonly XMediaSequenceTag? MediaSequenceTag;

    public readonly bool EndList = false;

    public MediaPlaylist(List<TagInfo> globalTags, List<MediaSegment> mediaSegments,
        XTargetDurationTag targetDurationTag, XMediaSequenceTag? mediaSequenceTag, bool endList)
    {
        this.GlobalTags = globalTags;
        this.MediaSegments = mediaSegments;
        this.TargetDurationTag = targetDurationTag;
        this.MediaSequenceTag = mediaSequenceTag;
        this.EndList = endList;
    }
}