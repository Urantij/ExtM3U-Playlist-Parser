namespace PlaylistParser.Tags.Media
{
    public class XProgramDateTimeTag : BaseTag
    {
        public readonly DateTimeOffset time;

        public XProgramDateTimeTag(string value) : base(value)
        {
            time = DateTimeOffset.Parse(value);
        }
    }
}