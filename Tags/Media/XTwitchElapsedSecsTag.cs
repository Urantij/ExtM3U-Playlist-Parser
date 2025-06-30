namespace ExtM3UPlaylistParser.Tags.Media;

public class XTwitchElapsedSecsTag : BaseTag
{
    public readonly float ElapsedSeconds;

    public XTwitchElapsedSecsTag(string value) : base(value)
    {
        ElapsedSeconds = float.Parse(value);
    }
}