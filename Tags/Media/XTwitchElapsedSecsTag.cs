namespace PlaylistParser.Tags.Media
{
    public class XTwitchElapsedSecsTag : BaseTag
    {
        public readonly float elapsedSeconds;

        public XTwitchElapsedSecsTag(string value) : base(value)
        {
            elapsedSeconds = float.Parse(value);
        }
    }
}