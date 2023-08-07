using DiscordBot.Utilities;

using YouTubeSearch;

namespace DiscordBot.Services.Youtube
{
    public class BotVideoInfo
    {
        private readonly VideoSearchComponents _videoInfo;

        public BotVideoInfo(VideoSearchComponents videoInfo, string dlUrl)
        {
            DownloadUrl = dlUrl?.WhiteSpaceToNull() ?? throw new ArgumentNullException(nameof(dlUrl));
            _videoInfo = videoInfo ?? throw new ArgumentNullException(nameof (videoInfo));
        }

        public string DownloadUrl { get; }

        internal VideoSearchComponents GetVideoInfo()
        {
            return _videoInfo;
        }
    }
}