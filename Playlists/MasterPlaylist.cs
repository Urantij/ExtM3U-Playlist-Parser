using ExtM3UPlaylistParser.Models;
using ExtM3UPlaylistParser.Tags.Master;

namespace ExtM3UPlaylistParser.Playlists
{
    public class MasterPlaylist
    {
        public readonly List<TagInfo> globalTags = new();
        public readonly List<VariantStream> variantStreams = new();
        /// <summary>
        /// Group-Id к тегам с медиа
        /// </summary>
        public readonly Dictionary<string, List<MediaTag>> mediaDict = new();

        public MasterPlaylist(List<TagInfo> globalTags, List<VariantStream> variantStreams, Dictionary<string, List<MediaTag>> mediaDict)
        {
            this.globalTags = globalTags;
            this.variantStreams = variantStreams;
            this.mediaDict = mediaDict;
        }
    }
}