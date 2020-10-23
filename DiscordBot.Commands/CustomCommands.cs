using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

using DiscordBot.Data;
using DiscordBot.Config;
using DiscordBot.Localization;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace DiscordBot.Commands
{
    [Group("Custom commands")]
    public class CustomCommands : ModuleBase<SocketCommandContext>, ICustomCommandHandler
    {
        private readonly CommandsRepository _commandsRepository;
        private readonly HashSet<string> _reservedCommands;
        private Dictionary<string, string> _commandResponse;

        public CustomCommands(CommandsRepository commandsRepository)
        {
            _commandsRepository = commandsRepository;
            _reservedCommands = new HashSet<string>();

            Initialize();
        }

        public async Task<bool> HandleCommand(SocketUserMessage commandMessage, SocketCommandContext context, int commandStart)
        {
            if (commandMessage.Author != context.Client.CurrentUser)
            {
                if (_commandResponse.TryGetValue(commandMessage.Content.Substring(commandStart), out string response))
                {
                    await commandMessage.Channel.SendMessageAsync(response);
                    return true;
                }
            }

            return false;
        }

        public bool AddCommand(string command, string response)
        {
            _commandResponse[command] = response;

            if (_commandResponse[command] == response)
            {
                return true;
            }

            return false;
        }

        public bool RemoveCommand(string command)
        {
            return _commandResponse.Remove(command);
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
            else if(_reservedCommands.Contains(command))
            {
                await Context.Channel.SendMessageAsync("That command is already in use by the bot. Pick a different one.");
                return;
            }
            else if(_commandResponse.ContainsKey(command))
            {
                await Context.Channel.SendMessageAsync("That custom command already exists. If you want to replace it, delete the old command first.");
                return;
            }

            if (_commandsRepository.AddNewCommand(command, response))
            {
                _commandResponse[command] = response;
                if (Uri.TryCreate(response, UriKind.Absolute, out Uri uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    response = $"<{response}>";
                }
                await Context.Channel.SendMessageAsync($"Added new command {command} with response {response}");
            }
            else
            {
                await Context.Channel.SendMessageAsync(Strings.Default_Error_Response);
            }
        }

        [Command("DeleteCommand"), Alias("dc"), RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task DeleteCommand(string command)
        {
            command = command.ToLower();

            if (string.IsNullOrEmpty(command))
            {
                await Context.Channel.SendMessageAsync("You can't delete nothing.");
                return;
            }
            else if (_reservedCommands.Contains(command))
            {
                await Context.Channel.SendMessageAsync("You can't delete a reserved command.");
                return;
            }

            if (_commandsRepository.DeleteCommand(command))
            {
                await Context.Channel.SendMessageAsync($"Deleted command {command}");
                _commandResponse.Remove(command);
            }
        }

        [Command("ListCommands"), Alias("lc", "ListCustomCommands", "ListCommand")]
        public async Task ListCommands()
        {
            var response = string.Join("\n", _commandResponse.Keys.ToList().OrderBy(x => x));
            await Context.Channel.SendMessageAsync(response);
        }

        private void Initialize()
        {
            _commandResponse = _commandsRepository.GetAllCustomCommands();

            using (var fs = new FileStream("ReservedCommands.txt", FileMode.Open))
            using (var sr = new StreamReader(fs))
            {
                foreach(var item in sr.ReadToEnd().Split(','))
                {
                    _reservedCommands.Add(item.ToLower());
                }
            }
        }
    }
}
