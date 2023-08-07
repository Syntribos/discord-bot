using DiscordBot.Services.Youtube;
using DiscordBot.Utilities;
using YoutubeDLSharp;
using YouTubeSearch;

using var tempFile = new TempDirectory(@"C:\Users\Jess\Desktop\tempy");
var ydl = new YoutubeDL();

var vdl = VideoDownloaderFactory.CreateDownloader(tempFile);
var service = new YoutubeService(new VideoSearch(), ydl) as IYoutubeService;
var result = (await service.Search("chelmico ずるいね", 1)).ToList().First();
Console.WriteLine(result.DownloadUrl);


var path = await service.DownloadVideo(vdl, result);
Console.WriteLine(path);