namespace PlaylistParser
{
    public class PlaylistException : Exception
    {
        public PlaylistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public PlaylistException(string message) : base(message)
        {
        }
    }
}