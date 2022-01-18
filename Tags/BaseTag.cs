namespace PlaylistParser.Tags
{
    /// <summary>
    /// Описывает определённый тип тегов
    /// </summary>
    public abstract class BaseTag
    {
        public readonly string value;

        public BaseTag(string value)
        {
            this.value = value;
        }
    }
}