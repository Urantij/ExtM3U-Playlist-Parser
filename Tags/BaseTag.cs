namespace ExtM3UPlaylistParser.Tags;

/// <summary>
/// Описывает определённый тип тегов
/// </summary>
public abstract class BaseTag
{
    /// <summary>
    /// Текст после тега
    /// </summary>
    public readonly string value;

    public BaseTag(string value)
    {
        this.value = value;
    }
}