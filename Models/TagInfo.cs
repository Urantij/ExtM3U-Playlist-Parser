namespace PlaylistParser.Models
{
    public class TagInfo
    {
        public readonly string tag;
        public readonly string? value;

        public TagInfo(string tag, string? value)
        {
            this.tag = tag;
            this.value = value;
        }
    }
}