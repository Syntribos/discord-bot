using Discord.Commands;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public interface ICustomCommandHandler
    {
        Task<bool> HandleCommand(SocketUserMessage commandMessage, SocketCommandContext context, int commandStart);

        string AddCommand(string command, string response);

        bool RemoveCommand(string command);

        bool HasCommand(string command);

        bool IsReserved(string command);

        string GetResponse(string command);

        List<string> GetAllCustomCommands();
    }
}
