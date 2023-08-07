using System.Reflection;

using DiscordBot.Config;

using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;

namespace DiscordBot.Commands
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;
        private readonly InteractionService _interactionService;
        private readonly IServiceProvider _services;
        private readonly ICustomCommandHandler _customCommandHandler;
        private readonly BotConfig _botConfig;

        public CommandHandler(DiscordSocketClient discordSocketClient, CommandService commandService, InteractionService interactionService, IServiceProvider services, ICustomCommandHandler customCommandHandler, BotConfig botConfig)
        {
            _client = discordSocketClient;
            _commandService = commandService;
            _interactionService = interactionService;
            _services = services;
            _customCommandHandler = customCommandHandler;
            _botConfig = botConfig;

            _client.Ready += ReadyAsync;
            _client.InteractionCreated += HandleInteraction;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commandService.AddModulesAsync(Assembly.Load("DiscordBot.Commands"), _services);
        }

        public async Task InstallInteractionsAsync()
        {
            await _interactionService.AddModulesAsync(Assembly.Load("DiscordBot.Commands"), _services);
        }

        private async Task HandleCommandAsync(SocketMessage msg)
        {
            if (msg is not SocketUserMessage message)
            {
                return;
            }

            var argPos = 0;

            if (!(message.HasStringPrefix(_botConfig.Prefix, ref argPos)
                || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
                || message.Author.IsBot)
            {
                return;
            }

            var context = new SocketCommandContext(_client, message);

            if (!await _customCommandHandler.HandleCommand(message, context, argPos))
            {
                await _commandService.ExecuteAsync(context, argPos, _services);
            }
        }

        private async Task ReadyAsync()
        {
            await _interactionService.RegisterCommandsGloballyAsync();
        }


        private async Task HandleInteraction(SocketInteraction interaction)
        {
            try
            {
                // Create an execution context that matches the generic type parameter of your InteractionModuleBase<T> modules.
                var context = new SocketInteractionContext(_client, interaction);

                // Execute the incoming command.
                var result = await _interactionService.ExecuteCommandAsync(context, _services);

                if (!result.IsSuccess)
                    switch (result.Error)
                    {
                        case InteractionCommandError.UnmetPrecondition:
                            // implement
                            break;
                        default:
                            break;
                    }
            }
            catch
            {
                // If Slash Command execution fails it is most likely that the original interaction acknowledgement will persist. It is a good idea to delete the original
                // response, or at least let the user know that something went wrong during the command execution.
                if (interaction.Type is InteractionType.ApplicationCommand)
                    await interaction.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
            }
        }
    }
}
