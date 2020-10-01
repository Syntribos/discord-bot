using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DiscordBot.Contracts;
using DiscordBot.Data;
using DiscordBot.DataModels.Events;
using DiscordBot.DataModels.Twitch;

namespace DiscordBot.Twitch
{
    public class TwitchClient
    {
        private readonly CancellationToken _token;
        private readonly StreamerDataRepository _streamerDataRepository;
        private Dictionary<string, string> _streamers;

        public TwitchClient(CancellationToken token, StreamerDataRepository streamerDataRepository)
        {
            Contract.RequireNotNull(token, nameof(token));
            Contract.RequireNotNull(streamerDataRepository, nameof(streamerDataRepository));

            _token = token;
            _streamerDataRepository = streamerDataRepository;
            
            SetupStreamers();
        }

        public async Task CheckLiveTwitchChannels(TimeSpan interval)
        {
            while (true)
            {
                _ = RunCheck();
                await Task.Delay(interval, _token);
            }
        }

        public void RemoveStreamerFromWatch(string username)
        {
            if (_streamers.Remove(username))
            {
                //Also remove the watch from the twitch library
            }
        }

        private async Task RunCheck()
        {
            if (!_streamers.Any())
            {
                return;
            }

            /*
            var title = "Mesmerchair has gone online!";
            var description = "";
            DiscordEmbed meh = CreateTwitchEmbed(title, DiscordColor.PhthaloBlue);
            meh.Author = "hello";
                Title: "test"
                );
                /*
                "username":"LIVE-Twitch",
                "avatar_url":"https://static-cdn.jtvnw.net/jtv_user_pictures/twitch-profile_image-8a8c5be2e3b64a9a-300x300.png",
                "content":" TEST ",
                "embeds": [{ "title": "Test", "description": "Test", "color": 0xffffff}]}
            }*/
        }

        private async Task Announce(string streamer, string announceMessage)
        {
            if (announceMessage.Contains("{0}"))
            {
                announceMessage = string.Format(announceMessage, streamer);
            }

            //await channel.SendMessageAsync(announceMessage);
        }

        private async void OnStreamerLive(StreamerWentLiveEventArgs args)
        {
            if (_streamers.TryGetValue(args.Streamer, out string announceMessage))
            {
                await Announce(args.Streamer, announceMessage);
            }
        }

        /*
        private DiscordEmbed CreateTwitchEmbed(string title, string description, DiscordColor colour)
        {
            var builder = new DiscordEmbedBuilder();
            builder.Title = title;
            builder.Description = description;
            builder.Color = colour;
            builder.Url = "stuff";


            return builder.Build();
        }*/

        private void SetupStreamers()
        {
            var streamers = _streamerDataRepository.GetStreamers();
            if (streamers.Any())
            {
                _streamers = streamers.ToDictionary(
                kvp => kvp.Name,
                kvp => kvp.AnnounceMessage ?? _streamerDataRepository.DefaultAnnounceMessage);
            }
            else
            {
                _streamers = new Dictionary<string, string>();
            }
        }
    }
}
