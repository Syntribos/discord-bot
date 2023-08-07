namespace DiscordBot.Services.Youtube;

public interface IVideoDownloader
{
    public Task<string> RunAudioDownload(IBotVideoInfo videoInfo);
}