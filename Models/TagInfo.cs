namespace ExtM3UPlaylistParser.Models;

public class TagInfo
{
    public readonly string Tag;
    public readonly string? Value;

    public TagInfo(string tag, string? value)
    {
        this.Tag = tag;
        this.Value = value;
    }
}