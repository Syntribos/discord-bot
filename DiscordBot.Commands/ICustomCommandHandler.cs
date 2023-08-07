using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Commands;

public interface ICustomCommandHandler
{
    Task<bool> HandleCommand(SocketUserMessage commandMessage, SocketCommandContext context, int commandStart);

    bool TryAddCommand(string command, string response);

    bool RemoveCommand(string command);

    bool HasCommand(string command);

    bool IsReserved(string command);

    string GetResponse(string command);

    List<string> GetAllCustomCommands();
}