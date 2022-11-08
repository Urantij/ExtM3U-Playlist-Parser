# PlaylistParser
EXTM3U playlist parser

Написано для работы с плейлистами твича.

TargetFramework net6.0

## Как использовать

```c#
MasterParser parser = new();
parser.UnknownLineFound += (_, line) => OnUnknownPlaylistLine(line);
parser.CommentLineFound += (_, line) => OnCommentPlaylistLine(line);

MasterPlaylist masterPlaylist = parser.Parse(playlistContentString);

...
...
...

MediaParser parser = new();
parser.UnknownLineFound += (_, line) => OnUnknownPlaylistLine(line);
parser.CommentLineFound += (_, line) => OnCommentPlaylistLine(line);

MediaPlaylist mediaPlaylist = parser.Parse(playlistContentString);
```

## Чего нет

Не все теги добавлены, из относительно важных BYTERANGE, какие-то там ещё есть, не знаю.
