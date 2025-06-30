using ExtM3UPlaylistParser.Models;
using ExtM3UPlaylistParser.Tags.Master;

namespace ExtM3UPlaylistParser.Playlists;

public class MasterPlaylist
{
    public readonly List<TagInfo> GlobalTags = new();
    public readonly List<VariantStream> VariantStreams = new();

    /// <summary>
    /// Group-Id к тегам с медиа
    /// </summary>
    public readonly Dictionary<string, List<MediaTag>> MediaDict = new();

    public MasterPlaylist(List<TagInfo> globalTags, List<VariantStream> variantStreams,
        Dictionary<string, List<MediaTag>> mediaDict)
    {
        this.GlobalTags = globalTags;
        this.VariantStreams = variantStreams;
        this.MediaDict = mediaDict;
    }
}