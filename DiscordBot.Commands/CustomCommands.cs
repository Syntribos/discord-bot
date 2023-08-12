using Discord;
using Discord.Commands;
using DiscordBot.Localization;

namespace DiscordBot.Commands
{
    public class CustomCommands : ModuleBase<ICommandContext>
    {
        private const int MAX_COMMAND_LENGTH = 20;
        private const int MAX_COMMAND_RESPONSE_LENGTH = 500;
        private readonly ICustomCommandHandler _customCommandHandler;

        public CustomCommands(ICustomCommandHandler customCommandHandler)
        {
            _customCommandHandler = customCommandHandler;
        }

        [Command("NewCommand"), Alias("nc", "addcommand", "ac"), RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task NewCommand(string command, [Remainder]string response)
        {
            command = command.ToLower();

            if (string.IsNullOrEmpty(command) || string.IsNullOrEmpty(response))
            {
                await Context.Channel.SendMessageAsync("Command response can't be empty you big dummy.");
                return;
            }
            
            if (_customCommandHandler.IsReserved(command))
            {
                await Context.Channel.SendMessageAsync("That command is already in use by the bot. Pick a different one.");
                return;
            }
            
            if (_customCommandHandler.HasCommand(command))
            {
                await Context.Channel.SendMessageAsync("That custom command already exists. If you want to replace it, delete the old command first.");
                return;
            }

            if (command.Length > MAX_COMMAND_LENGTH || response.Length > MAX_COMMAND_RESPONSE_LENGTH)
            {
                await Context.Channel.SendMessageAsync($"Command must be {MAX_COMMAND_LENGTH} characters or less, and the response must be {MAX_COMMAND_RESPONSE_LENGTH} or less.");
                return;
            }

            if (_customCommandHandler.TryAddCommand(command, response))
            {
                await Context.Channel.SendMessageAsync($"Added new command {command} with response {_customCommandHandler.GetResponse(command)}");
            }
            else
            {
                await Context.Channel.SendMessageAsync(Strings.Default_Error_Response);
            }
        }

        [Command("DeleteCommand"), Alias("dc"), RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task DeleteCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                await Context.Channel.SendMessageAsync("You can't delete nothing.");
                return;
            }
            else if (_customCommandHandler.IsReserved(command))
            {
                await Context.Channel.SendMessageAsync("You can't delete a reserved command.");
                return;
            }

            if (_customCommandHandler.RemoveCommand(command))
            {
                await Context.Channel.SendMessageAsync($"Deleted command {command}");
            }
        }

        [Command("ListCommands"), Alias("lc", "ListCustomCommands", "ListCommand")]
        public async Task ListCommands()
        {
            var response = string.Join("\n", _customCommandHandler.GetAllCustomCommands());
            await Context.Channel.SendMessageAsync(response);
        }
    }
}