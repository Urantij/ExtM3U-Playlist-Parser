namespace ExtM3UPlaylistParser.Parsers
{
    public abstract class BaseParser<T>
        where T : class
    {
        protected static readonly HashSet<string> basicPlaylistTags = new()
        {
            "#EXTM3U",
            "#EXT-X-VERSION"
        };

        protected static readonly HashSet<string> mediaSegmentTags = new()
        {
            "#EXTINF",
            "#EXT-X-BYTERANGE",
            "#EXT-X-DISCONTINUITY",
            "#EXT-X-KEY",
            "#EXT-X-MAP",
            "#EXT-X-PROGRAM-DATE-TIME",
            "#EXT-X-DATERANGE"
        };

        protected static readonly HashSet<string> mediaPlaylistTags = new()
        {
            "#EXT-X-TARGETDURATION",
            "#EXT-X-MEDIA-SEQUENCE",
            "#EXT-X-DISCONTINUITY-SEQUENCE",
            "#EXT-X-ENDLIST",
            "#EXT-X-PLAYLIST-TYPE",
            "#EXT-X-I-FRAMES-ONLY"
        };

        protected static readonly HashSet<string> masterPlaylistTags = new()
        {
            "#EXT-X-MEDIA",
            "#EXT-X-STREAM-INF",
            "#EXT-X-I-FRAME-STREAM-INF",
            "#EXT-X-SESSION-DATA",
            "#EXT-X-SESSION-KEY"
        };

        protected static readonly HashSet<string> mediaOrMasterPlaylistTags = new()
        {
            "#EXT-X-INDEPENDENT-SEGMENTS",
            "#EXT-X-START"
        };

        protected static readonly string[] listLinesSeparator = new string[] { "\r\n", "\n" };

        /// <summary>
        /// Комментарий это строка, начинающаяся с #, но не с #EXT
        /// </summary>
        public event EventHandler<string>? CommentLineFound;
        public event EventHandler<string>? UnknownLineFound;

        /* TODO проблема, шо поля плейлистов публично доступны для изменений.
         * Можно копировать все поля в парсер, класть их все разом и ридоинл
         * Или Можно сделать гет интернал сет */
        /// <summary>
        /// Вызывает преобразования.
        /// 
        /// Если будет ошибка при парсинге, может кинуть какую-нибудь ошибку.
        /// Но для нормальных плейлистов не кидает, вроде.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="PlaylistException"></exception>
        /// <exception cref="Exception"></exception>
        public abstract T Parse(string text);

        protected void StartParsing(string text)
        {
            try
            {
                /* Lines in a Playlist file are terminated by either a single line feed
                 * character or a carriage return character followed by a line feed
                 * character.  Each line is a URI, is blank, or starts with the
                 * character '#'.  Blank lines are ignored.  Whitespace MUST NOT be
                 * present, except for elements in which it is explicitly specified. */

                string[] lines = text.Split(listLinesSeparator, StringSplitOptions.None);

                // Если это не так, хз, на что мы вообще смотрим.
                if (lines[0] != "#EXTM3U") throw new PlaylistException($"\"#EXTM3U\" != \"{lines[0]}\"");

                // Не уверен, зачем.
                if (lines.Length == 1) throw new PlaylistException($"1 line length");

                for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
                {
                    string line = lines[lineIndex];

                    if (line == "") continue;

                    if (line.StartsWith("#EXT"))
                    {
                        //#EXT-X-DATERANGE:ID="quartile-1604230022-0",CLASS="twitch-ad-quartile",START-DATE="2020-11-01T11:27:02.104Z",DURATION=2.002,X-TV-TWITCH-AD-QUARTILE="0"

                        string tag;
                        string? value;

                        int tagSeparatorIndex = line.IndexOf(':');
                        if (tagSeparatorIndex != -1)
                        {
                            tag = line[..tagSeparatorIndex];
                            value = line[(tagSeparatorIndex + 1)..];
                        }
                        else
                        {
                            tag = line;
                            value = null;
                        }

                        OnTagLine(tag, value);
                        continue;
                    }
                    else if (line[0] == '#')
                    {
                        CommentLineFound?.Invoke(this, line);
                        continue;
                    }

                    if (Uri.TryCreate(line, UriKind.RelativeOrAbsolute, out Uri? result))
                    {
                        OnUriLine(line, result);
                    }
                    else UnknownLineFound?.Invoke(this, line);
                }

                ContinueParsing();
            }
            catch (PlaylistException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new PlaylistException("Parse General Exception", e);
            }
        }

        protected virtual void ContinueParsing() { }

        protected virtual void OnTagLine(string tag, string? value) { }

        protected virtual void OnUriLine(string tag, Uri uri) { }
    }
}