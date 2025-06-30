using System.Globalization;

namespace ExtM3UPlaylistParser.Models;

public class Resolution
{
    public readonly int Width;
    public readonly int Height;

    public Resolution(int width, int height)
    {
        this.Width = width;
        this.Height = height;
    }

    /// <param name="value">1600x900</param>
    public static Resolution Parse(string value)
    {
        string[] split = value.Split('x');

        return new Resolution(int.Parse(split[0], CultureInfo.InvariantCulture),
            int.Parse(split[1], CultureInfo.InvariantCulture));
    }
}