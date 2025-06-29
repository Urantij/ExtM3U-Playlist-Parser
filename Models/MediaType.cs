namespace ExtM3UPlaylistParser.Models;

static class MediaType
{
    public const string AUDIO = "AUDIO";
    public const string VIDEO = "VIDEO";
    public const string SUBTITLES = "SUBTITLES";
    public const string CLOSED_CAPTIONS = "CLOSED-CAPTIONS";

    /// <exception cref="PlaylistException">Если какой то мутный тип</exception>
    public static string FindType(string type)
    {
        return type switch
        {
            AUDIO or VIDEO or SUBTITLES or CLOSED_CAPTIONS => type,

            _ => throw new PlaylistException("Unknown media type"),
        };
    }
}