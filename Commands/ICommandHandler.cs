using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public interface ICustomCommandHandler
    {
        Task<bool> HandleCommand(SocketUserMessage commandMessage, SocketCommandContext context);
    }
}
