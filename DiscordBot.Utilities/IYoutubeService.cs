using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBot.Utilities
{
    public interface IYoutubeService
    {
        Task<List<string>> DownloadVideos(string tempDir, string searchQuery);
    }
}
