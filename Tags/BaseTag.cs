namespace ExtM3UPlaylistParser.Tags;

/// <summary>
/// Описывает определённый тип тегов
/// </summary>
public abstract class BaseTag
{
    /// <summary>
    /// Текст после тега
    /// </summary>
    public readonly string Value;

    public BaseTag(string value)
    {
        this.Value = value;
    }
}