using YoutubeDLSharp;

namespace DiscordBot.Services.Youtube;

public class VideoDownloader : IVideoDownloader
{
    private readonly YoutubeDL _downloader;

    internal VideoDownloader(YoutubeDL downloader)
    {
        _downloader = downloader;
    }

    public async Task<string> RunAudioDownload(IBotVideoInfo videoInfo)
    {
        var result = await _downloader.RunAudioDownload(videoInfo.DownloadUrl);
        return result.Success
            ? result.Data
            : throw new Exception(
                $"Failed to download video {videoInfo}: {string.Join('\n', result.ErrorOutput.ToList())}");
    }
}