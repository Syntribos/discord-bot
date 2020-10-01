using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Utilities
{
    public class VideoToAudioConverter : IVideoToAudioConverter
    {
        public IEnumerable<string> ConvertVideoToMp3(List<string> videoPaths)
        {
            foreach (var video in videoPaths)
            {
                yield return ConvertVideoToMp3(video);
            }
        }

        public string ConvertVideoToMp3(string videoPath)
        {
            var baseFilePath = Path.GetDirectoryName(videoPath);
            var videoName = Path.GetFileNameWithoutExtension(videoPath);
            var mp3FilePath = Path.Combine(baseFilePath, videoName + ".mp3");

            var ffmpegStartInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $" -i \"{videoPath}\" -vn -ab 320k \"{mp3FilePath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };

            var ffmpeg = new Process { StartInfo = ffmpegStartInfo };
            ffmpeg.Start();
            ffmpeg.WaitForExit();

            return mp3FilePath;
        }
    }
}
