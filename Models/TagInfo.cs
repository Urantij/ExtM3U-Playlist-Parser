namespace PlaylistParser.Models
{
    public class TagInfo
    {
        public readonly string tag;
        public readonly string? attributes;

        public TagInfo(string tag, string? attributes)
        {
            this.tag = tag;
            this.attributes = attributes;
        }
    }
}