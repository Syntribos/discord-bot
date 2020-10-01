using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly CommandService _commandService;
        private readonly IServiceProvider _services;
        private readonly BotConfig _botConfig;

        public CommandHandler(DiscordSocketClient discordSocketClient, CommandService commandService, IServiceProvider services, BotConfig botConfig)
        {
            _discordSocketClient = discordSocketClient;
            _commandService = commandService;
            _services = services;
            _botConfig = botConfig;
        }

        public async Task InstallCommandsAsync()
        {
            _discordSocketClient.MessageReceived += HandleCommandAsync;

            await _commandService.AddModulesAsync(Assembly.GetAssembly(typeof(CommandHandler)), _services);
            /*_commandService.AddModuleAsync<TwitchCommands>(serviceProvider);
            _commandService.RegisterCommands<TwitchCommands>();
            _commandService.RegisterCommands<AdminCommands>();
            _commandService.RegisterCommands<CustomCommands>();
            _commandService.RegisterCommands<YoutubeCommands>();*/
        }

        public async Task HandleCommandAsync(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;
            if (message == null)
            {
                return;
            }

            int argPos = 0;

            if (!(message.HasStringPrefix(_botConfig.Prefix, ref argPos)
                || message.HasMentionPrefix(_discordSocketClient.CurrentUser, ref argPos))
                || message.Author.IsBot)
            {
                return;
            }

            var context = new SocketCommandContext(_discordSocketClient, message);

            var result = await _commandService.ExecuteAsync(context, argPos, _services);

            Console.WriteLine("asd");
        }
    }
}
