using ExtM3UPlaylistParser.Exceptions;

namespace ExtM3UPlaylistParser.Models;

static class MediaType
{
    public const string Audio = "AUDIO";
    public const string Video = "VIDEO";
    public const string Subtitles = "SUBTITLES";
    public const string ClosedCaptions = "CLOSED-CAPTIONS";

    /// <exception cref="PlaylistException">Если какой то мутный тип</exception>
    public static string FindType(string type)
    {
        return type switch
        {
            Audio or Video or Subtitles or ClosedCaptions => type,

            _ => throw new PlaylistException("Unknown media type"),
        };
    }
}