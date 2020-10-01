using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YouTubeSearch;

namespace DiscordBot.Utilities
{
    public class YoutubeService : IYoutubeService
    {
        private readonly VideoSearch _videoSearch = new VideoSearch();

        public async Task<List<string>> DownloadVideos(string outputPath, string searchQuery)
        {
            var videoPaths = new List<string>();
            var videoPath = DownloadVideoAndReturnPath((await _videoSearch.GetVideos(searchQuery, 1)).First(), outputPath);
            videoPaths.Add(videoPath);
            return videoPaths;
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

            var request = (HttpWebRequest)WebRequest.Create(toDownload.DownloadUrl);
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
