using System;

namespace DiscordBot.DataModels.Events
{
    public class StreamerWentLiveEventArgs : EventArgs
    {
        public StreamerWentLiveEventArgs(string streamerName)
        {
            Streamer = streamerName;
        }

        public string Streamer { get; }
    }
}
