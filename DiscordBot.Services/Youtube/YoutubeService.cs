using System.Net;

using YoutubeDLSharp;
using YouTubeSearch;

namespace DiscordBot.Services.Youtube
{
    public class YoutubeService : IYoutubeService
    {
        private readonly YoutubeDL _youtubeDl;
        private readonly VideoSearch _videoSearch;

        public YoutubeService(VideoSearch videoSearch, YoutubeDL youtubeDl)
        {
            _videoSearch = videoSearch ?? throw new ArgumentNullException(nameof(videoSearch));
            _youtubeDl = youtubeDl ?? throw new ArgumentNullException(nameof(youtubeDl));
        }

        public async Task<IEnumerable<BotVideoInfo>> Search(string query, int pageNumber)
        {
            var videoInfoList = await _videoSearch.GetVideosPaged(query, pageNumber);
            return videoInfoList.Select(x => new BotVideoInfo(x, string.Empty));
        }

        public IEnumerable<string> DownloadVideo(string outputPath, BotVideoInfo video)
        {
            return Enumerable.Empty<string>();
        }

        private string DownloadVideoAndReturnPath(VideoSearchComponents video, string outputPath)
        {
            var toDownload = DownloadUrlResolver.GetDownloadUrls(video.getUrl())
                .OrderBy(x => x.FileSize)
                .First(x => x.AudioBitrate > 0 && x.FileSize > 0);
            var dl = new VideoDownloader();
            var videoExtension = toDownload.VideoExtension;

            if (toDownload.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(toDownload);
            }

            var basePath = Path.Combine(outputPath, Path.GetRandomFileName());

#pragma warning disable SYSLIB0014 // Type or member is obsolete
            var request = (HttpWebRequest)WebRequest.Create(toDownload.DownloadUrl);
#pragma warning restore SYSLIB0014 // Type or member is obsolete
            var response = (HttpWebResponse)request.GetResponse();

            var outFilePath = basePath + videoExtension;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var ms = new MemoryStream();
                response.GetResponseStream().CopyTo(ms);
                var outFile = File.Create(outFilePath);
                ms.WriteTo(outFile);
                outFile.Close();
            }
            return outFilePath;
        }
    }
}
