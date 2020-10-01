using System;
using System.Threading.Tasks;

using DiscordBot.Data;
using DiscordBot.Localization;
using DiscordBot.Config;

using Discord.Commands;

namespace DiscordBot.Commands
{
    public class AdminCommands : ModuleBase<SocketCommandContext>
    {
        private readonly SettingsRepository _settingsRepository;
        private readonly BotConfig _botConfig;

        public AdminCommands(SettingsRepository settingsRepository, BotConfig botConfig)
        {
            _settingsRepository = settingsRepository;
            _botConfig = botConfig;
        }

        [Command("Ping")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync($"Pong! {Context.Client.Latency}ms");
        }

        [Command("Prefix"), Alias("ChangePrefix")]
        public async Task ChangePrefix(string newPrefix)
        {
            if (string.IsNullOrEmpty(newPrefix))
            {
                await Context.Channel.SendMessageAsync("Your new prefix can't be empty!");
                return;
            }
            else if (newPrefix.Length > 2)
            {
                await Context.Channel.SendMessageAsync("Your prefix can't be more than 2 characters!");
                return;
            }

            try
            {
                if (_settingsRepository.ChangePrefix(newPrefix))
                {
                    _botConfig.Prefix = newPrefix;
                    await Context.Channel.SendMessageAsync($"Your new prefix was changed to {newPrefix}");
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
    }
}
