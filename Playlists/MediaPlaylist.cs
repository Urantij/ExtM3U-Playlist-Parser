using PlaylistParser.Models;
using PlaylistParser.Tags.Media;

namespace PlaylistParser.Playlists
{
    public class MediaPlaylist
    {
        public readonly List<TagInfo> globalTags = new();
        public readonly List<MediaSegment> mediaSegments = new();

        public readonly XTargetDurationTag targetDurationTag;
        public readonly XMediaSequenceTag? mediaSequenceTag;

        public readonly bool endList = false;

        public MediaPlaylist(List<TagInfo> globalTags, List<MediaSegment> mediaSegments, XTargetDurationTag targetDurationTag, XMediaSequenceTag? mediaSequenceTag, bool endList)
        {
            this.globalTags = globalTags;
            this.mediaSegments = mediaSegments;
            this.targetDurationTag = targetDurationTag;
            this.mediaSequenceTag = mediaSequenceTag;
            this.endList = endList;
        }
    }
}