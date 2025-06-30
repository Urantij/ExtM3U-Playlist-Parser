using ExtM3UPlaylistParser.Tags.Master;

namespace ExtM3UPlaylistParser.Models;

/// <summary>
/// В то время, как стриминфтег репрезентует собой вариант стрим, этот класс репрезентует собой стриминфтег + с ним связанные теги
/// И юрл, который следует за этим тегом
/// </summary>
public class VariantStream
{
    public readonly Uri Uri;

    public readonly XStreamInfTag StreamInfTag;

    /// <summary>
    /// Нулл, если медиа нет
    /// </summary>
    public List<MediaTag>? VideoMediaTags;

    /// <summary>
    /// Нулл, если медиа нет
    /// </summary>
    public List<MediaTag>? AudioMediaTags;

    /// <summary>
    /// Нулл, если медиа нет
    /// </summary>
    public List<MediaTag>? SubtitlesMediaTags;

    /// <summary>
    /// Нулл, если медиа нет
    /// </summary>
    public List<MediaTag>? ClosedCaptionsMediaTags;

    public VariantStream(Uri uri, XStreamInfTag streamInfCommand)
    {
        this.Uri = uri;
        this.StreamInfTag = streamInfCommand;
    }
}