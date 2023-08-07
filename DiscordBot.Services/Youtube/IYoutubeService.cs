namespace DiscordBot.Services.Youtube;

public interface IYoutubeService
{
    Task<IEnumerable<BotVideoInfo>> Search(string query, int pageNumber);

    IEnumerable<string> DownloadVideo(string outputPath, BotVideoInfo video);
}