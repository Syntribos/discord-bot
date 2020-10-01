using System;
using System.Threading.Tasks;

using DiscordBot.Contracts;
using DiscordBot.Data;

using System.Text.RegularExpressions;
using DiscordBot.Localization;
using Discord.Commands;
using DiscordBot.Twitch;

namespace DiscordBot.Commands
{
    public class TwitchCommands : ModuleBase<ICommandContext>
    {
        private const string VALID_USERNAME_PATTERN = "^[a-zA-Z0-9_]{4,25}$";
        private readonly Regex _usernameRegex = new Regex(VALID_USERNAME_PATTERN);
        private readonly StreamerDataRepository _streamerDataRepository;
        private readonly TwitchClient _twitchClient;

        public TwitchCommands(StreamerDataRepository streamerDataRepository, TwitchClient twitchClient)
        {
            Contract.RequireNotNull(streamerDataRepository, nameof(streamerDataRepository));
            Contract.RequireNotNull(twitchClient, nameof(twitchClient));

            _streamerDataRepository = streamerDataRepository;
            _twitchClient = twitchClient;
        }

        [Command("DefaultLiveMessage"), Alias("dlm")]
        public async Task ChangeDefaultLiveMessage([Remainder]string newMessage)
        {
            if (string.IsNullOrEmpty(newMessage))
            {
                await Context.Channel.SendMessageAsync("Your new live message can't be empty!");
                return;
            }
            else if (!newMessage.Contains("{0}"))
            {
                await Context.Channel.SendMessageAsync("The default live message must contain the pattern \"{0}\" where you want the streamer name to appear.");
                return;
            }

            try
            {
                if (_streamerDataRepository.UpdateDefaultLiveMessage(newMessage))
                {
                    await Context.Channel.SendMessageAsync(string.Format(Strings.LiveMessageUpdate_Success, newMessage));
                }
                else
                {
                    await Context.Channel.SendMessageAsync(Strings.Default_Error_Response);
                }
                
            }
            catch (Exception)
            {
                await Context.Channel.SendMessageAsync(Strings.Default_Error_Response);
            }
        }

        [Command("AddStreamer"), Alias("as")]
        public async Task AddStreamer(string username, [Remainder]string goLiveMessage)
        {
            if (!_usernameRegex.IsMatch(username))
            {
                await Context.Channel.SendMessageAsync($"{username} is not a valid Twitch username.");
                return;
            }

            try
            {
                bool result;
                goLiveMessage = goLiveMessage ?? string.Empty;
                bool custom = !string.IsNullOrEmpty(goLiveMessage);

                if (goLiveMessage.Contains("{0}"))
                {
                    goLiveMessage = string.Format(goLiveMessage, username);
                }

                result = _streamerDataRepository.AddStreamer(username, goLiveMessage);

                if (result)
                {
                    var endText = custom
                        ? $"announcement: {goLiveMessage}"
                        : "default announcement";
                    await Context.Channel.SendMessageAsync($"Successfully added {username} to the list of streamers to watch for with the {endText}!");
                }
                else
                {
                    await Context.Channel.SendMessageAsync(Strings.Default_Error_Response);
                }
            }
            catch (Exception)
            {
                await Context.Channel.SendMessageAsync(Strings.Default_Error_Response);
            }
        }

        [Command("RemoveStreamer"), Alias("rs")]
        public async Task RemoveStreamer(string username)
        {
            if (!_usernameRegex.IsMatch(username))
            {
                await Context.Channel.SendMessageAsync($"{username} is not a valid Twitch username.");
                return;
            }

            try
            {
                var result = _streamerDataRepository.RemoveStreamer(username);

                if (!result)
                {
                    await Context.Channel.SendMessageAsync($"Failed to remove {username}. Ensure you spelled the name correctly and try again.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"Successfully removed {username} from the list of streamers to watch for.");
                    _twitchClient.RemoveStreamerFromWatch(username);
                }
            }
            catch (Exception)
            {
                await Context.Channel.SendMessageAsync(Strings.Default_Error_Response);
            }
        }
    }
}
