namespace DiscordBot.Services.Youtube;

public interface IYoutubeService
{
    Task<IEnumerable<BotVideoInfo>> Search(string query, int pageNumber);

    Task<string> DownloadVideo(IVideoDownloader videoDownloader, IBotVideoInfo video);
}