namespace PlaylistParser.Tags.Media
{
    public class XTargetDurationTag : BaseTag
    {
        public readonly int targetDuration;

        public XTargetDurationTag(string value) : base(value)
        {
            targetDuration = int.Parse(value);
        }
    }
}