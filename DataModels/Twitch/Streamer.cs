namespace DiscordBot.DataModels.Twitch
{
    public class Streamer
    {
        public Streamer (string name, string announceMessage)
        {
            Name = name;
            AnnounceMessage = announceMessage;
            IsLive = false;
        }

        public string AnnounceMessage { get; }

        public bool IsLive { get; set; }

        public string Name { get; }
    }
}
