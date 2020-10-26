using Discord;
using Discord.Commands;
using DiscordBot.Localization;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class CustomCommands : ModuleBase<ICommandContext>
    {
        private readonly ICustomCommandHandler _customCommandHandler;

        public CustomCommands(ICustomCommandHandler customCommandHandler)
        {
            _customCommandHandler = customCommandHandler;
        }

        [Command("NewCommand"), Alias("nc", "addcommand", "ac"), RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task NewCommand(string command, [Remainder]string response)
        {
            if (string.IsNullOrEmpty(command) || string.IsNullOrEmpty(response))
            {
                await Context.Channel.SendMessageAsync("Command response can't be empty you big dummy.");
                return;
            }
            else if (_customCommandHandler.IsReserved(command))
            {
                await Context.Channel.SendMessageAsync("That command is already in use by the bot. Pick a different one.");
                return;
            }
            else if (_customCommandHandler.HasCommand(command))
            {
                await Context.Channel.SendMessageAsync("That custom command already exists. If you want to replace it, delete the old command first.");
                return;
            }

            response = _customCommandHandler.AddCommand(command, response);
            if (response != null)
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
