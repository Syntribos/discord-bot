namespace DiscordBot.Services
{
    public interface IVideoToAudioConverter
    {
        IEnumerable<string> ConvertVideoToMp3(List<string> videoPaths);

        string ConvertVideoToMp3(string videoPath);
    }
}