namespace DiscordBot.DataModels.Twitch
{
    public class StreamerFactory
    {
        public StreamerFactory()
            : this("{0} just went dsasdad")
        {
        }

        public StreamerFactory(string defaultAnnounce)
        {
            DefaultAnnounce = defaultAnnounce;
        }

        public string DefaultAnnounce { get; }

        public Streamer Create(string name)
        {
            return new Streamer(name, DefaultAnnounce);
        }

        public Streamer Create(string name, string announceMessage)
        {
            if (string.IsNullOrEmpty(announceMessage))
            {
                return new Streamer(name, DefaultAnnounce);
            }

            return new Streamer(name, announceMessage);
        }
    }
}
