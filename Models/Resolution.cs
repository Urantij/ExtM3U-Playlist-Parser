using System.Globalization;

namespace ExtM3UPlaylistParser.Models;

public class Resolution
{
    public readonly int width;
    public readonly int height;

    public Resolution(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    /// <param name="value">1600x900</param>
    public static Resolution Parse(string value)
    {
        string[] split = value.Split('x');

        return new Resolution(int.Parse(split[0], CultureInfo.InvariantCulture),
            int.Parse(split[1], CultureInfo.InvariantCulture));
    }
}