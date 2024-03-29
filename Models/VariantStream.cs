using ExtM3UPlaylistParser.Tags.Master;

namespace ExtM3UPlaylistParser.Models
{
    /// <summary>
    /// В то время, как стриминфтег репрезентует собой вариант стрим, этот класс репрезентует собой стриминфтег + с ним связанные теги
    /// И юрл, который следует за этим тегом
    /// </summary>
    public class VariantStream
    {
        public readonly Uri uri;

        public readonly XStreamInfTag streamInfTag;
        /// <summary>
        /// Нулл, если медиа нет
        /// </summary>
        public List<MediaTag>? videoMediaTags;
        /// <summary>
        /// Нулл, если медиа нет
        /// </summary>
        public List<MediaTag>? audioMediaTags;
        /// <summary>
        /// Нулл, если медиа нет
        /// </summary>
        public List<MediaTag>? subtitlesMediaTags;
        /// <summary>
        /// Нулл, если медиа нет
        /// </summary>
        public List<MediaTag>? closedCaptionsMediaTags;

        public VariantStream(Uri uri, XStreamInfTag streamInfCommand)
        {
            this.uri = uri;
            this.streamInfTag = streamInfCommand;
        }
    }
}