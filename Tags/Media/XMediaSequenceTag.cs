using System.Globalization;

namespace ExtM3UPlaylistParser.Tags.Media
{
    /// <summary>
    /// https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.3.2
    /// </summary>
    public class XMediaSequenceTag : BaseTag
    {
        public readonly int mediaSequenceNumber;

        public XMediaSequenceTag(string value) : base(value)
        {
            mediaSequenceNumber = int.Parse(value, CultureInfo.InvariantCulture);
        }
    }
}