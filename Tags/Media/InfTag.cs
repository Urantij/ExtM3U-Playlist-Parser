using System.Globalization;

namespace ExtM3UPlaylistParser.Tags.Media
{
    /// <summary>
    /// https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.2.1
    /// </summary>
    public class InfTag : BaseTag
    {
        public readonly float duration;
        public readonly string? title;

        public InfTag(string value)
            : base(value)
        {
            int separatorIndex = value.IndexOf(',');

            if (separatorIndex == -1)
            {
                duration = float.Parse(value);
                return;
            }

            duration = float.Parse(value[..separatorIndex], CultureInfo.InvariantCulture);
            title = value[(separatorIndex + 1)..];
        }
    }
}