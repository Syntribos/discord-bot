using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DiscordBot.Data;
using DiscordBot.Config;
using DiscordBot.Localization;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Linq;
using System.IO;

namespace DiscordBot.Commands
{
    [Group("Custom commands")]
    public class CustomCommandHandler : ICustomCommandHandler
    {
        private readonly CommandsRepository _commandsRepository;
        private readonly HashSet<string> _reservedCommands;
        private Dictionary<string, string> _commandResponse;

        public CustomCommandHandler(CommandsRepository commandsRepository)
        {
            _commandsRepository = commandsRepository;
            _reservedCommands = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
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
            if(_commandsRepository.AddNewCommand(command, response))
            {
                if (Uri.TryCreate(response, UriKind.Absolute, out Uri uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    response = $"<{response}>";
                }

                _commandResponse[command] = response;
                return true;
            }

            return false;
        }

        public bool HasCommand(string command)
        {
            return _commandResponse.ContainsKey(command);
        }

        public bool RemoveCommand(string command)
        {
            if (_commandsRepository.DeleteCommand(command))
            {
                _commandResponse.Remove(command);
                return true;
            }

            return false;
        }

        public bool IsReserved(string command)
        {
            return _reservedCommands.Contains(command);
        }

        public string GetResponse(string command)
        {
            return _commandResponse[command];
        }

        public List<string> GetAllCustomCommands()
        {
            return _commandResponse.Keys.OrderBy(x => x).ToList();
        }

        private void Initialize()
        {
            _commandResponse = _commandsRepository.GetAllCustomCommands();

            using (var fs = new FileStream("ReservedCommands.txt", FileMode.Open))
            using (var sr = new StreamReader(fs))
            {
                foreach (var item in sr.ReadToEnd().Split(','))
                {
                    _reservedCommands.Add(item.ToLower());
                }
            }
        }
    }
}
