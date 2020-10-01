using System.Collections.Generic;

namespace DiscordBot.Utilities
{
    public interface IVideoToAudioConverter
    {
        IEnumerable<string> ConvertVideoToMp3(List<string> videoPaths);

        string ConvertVideoToMp3(string videoPath);
    }
}