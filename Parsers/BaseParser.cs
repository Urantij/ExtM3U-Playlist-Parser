using ExtM3UPlaylistParser.Exceptions;

namespace ExtM3UPlaylistParser.Parsers;

public abstract class BaseParser<T>
    where T : class
{
    protected static readonly HashSet<string> BasicPlaylistTags = new()
    {
        "#EXTM3U",
        "#EXT-X-VERSION"
    };

    /// <summary>
    /// Применяются только к следующему медиа сегменту.
    /// </summary>
    protected static readonly HashSet<string> MediaSegmentTags = new()
    {
        "#EXTINF",
        "#EXT-X-BYTERANGE",
        "#EXT-X-DISCONTINUITY",
        "#EXT-X-PROGRAM-DATE-TIME"
    };

    /// <summary>
    /// На весь плейлист
    /// </summary>
    protected static readonly HashSet<string> MediaPlaylistTags = new()
    {
        "#EXT-X-TARGETDURATION",
        "#EXT-X-MEDIA-SEQUENCE",
        "#EXT-X-DISCONTINUITY-SEQUENCE",
        "#EXT-X-ENDLIST",
        "#EXT-X-PLAYLIST-TYPE",
        "#EXT-X-I-FRAMES-ONLY",
        // https://datatracker.ietf.org/doc/html/rfc8216#section-4.3.2.7 тут он как медиа сегмент атрибут, но по факту.. не понимаю
        "#EXT-X-DATERANGE"
    };
    
    /// <summary>
    /// Применяются ко всех медиа сегментам, которые идут после этого тега
    /// </summary>
    protected static readonly HashSet<string> MediaPlaylistRollingTags = new()
    {
        // Тут немного сложно, так как роляет формат... но я это делать не буду :)
        "#EXT-X-KEY",
        "#EXT-X-MAP",
    };

    protected static readonly HashSet<string> MasterPlaylistTags = new()
    {
        "#EXT-X-MEDIA",
        "#EXT-X-STREAM-INF",
        "#EXT-X-I-FRAME-STREAM-INF",
        "#EXT-X-SESSION-DATA",
        "#EXT-X-SESSION-KEY"
    };

    protected static readonly HashSet<string> MediaOrMasterPlaylistTags = new()
    {
        "#EXT-X-INDEPENDENT-SEGMENTS",
        "#EXT-X-START"
    };

    protected static readonly string[] ListLinesSeparator = new string[] { "\r\n", "\n" };

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

            string[] lines = text.Split(ListLinesSeparator, StringSplitOptions.None);

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

    protected virtual void ContinueParsing()
    {
    }

    protected virtual void OnTagLine(string tag, string? value)
    {
    }

    protected virtual void OnUriLine(string tag, Uri uri)
    {
    }
}