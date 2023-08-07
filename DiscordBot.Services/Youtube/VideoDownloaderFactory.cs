using DiscordBot.Utilities;
using YoutubeDLSharp;

namespace DiscordBot.Services.Youtube;

public static class VideoDownloaderFactory
{
    public static IVideoDownloader CreateDownloader(TempDirectory temporaryDirectory)
    {
        var youtubeDl = new YoutubeDL
        {
            OutputFolder = temporaryDirectory.DirectoryPath,
        };

        return new VideoDownloader(youtubeDl);
    }
}