using DiscordBot.Data;
using Discord.WebSocket;
using Discord.Commands;

namespace DiscordBot.Commands;

public class CustomCommandHandler : ICustomCommandHandler
{
    /*
    private const ulong JESS_ID = 91037904133963776;
    private const ulong GHOSTIE_ID = 178915990954967040;
    */

    private readonly CommandsRepository _commandsRepository;
    private readonly HashSet<string> _reservedCommands;
    private readonly Dictionary<string, string> _commandResponse;

    public CustomCommandHandler(CommandsRepository commandsRepository)
    {
        _commandsRepository = commandsRepository ?? throw new ArgumentNullException(nameof(commandsRepository));
        _reservedCommands = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        _commandResponse = new Dictionary<string, string>();
        Initialize();
    }

    public async Task<bool> HandleCommand(SocketUserMessage commandMessage, SocketCommandContext context, int commandStart)
    {
        if (commandMessage.Author.Id == context.Client.CurrentUser.Id)
        {
            return false;
        }

        var commandText = commandMessage.Content[commandStart..]?.ToLower() ?? string.Empty;

        if (!_commandResponse.TryGetValue(commandText, out var response))
        {
            return false;
        }

        await commandMessage.Channel.SendMessageAsync(response);
        return true;

    }

    public bool TryAddCommand(string command, string response)
    {
        if (!_commandsRepository.AddNewCommand(command, response))
        {
            return false;
        }

        _commandResponse[command] = response;
        return true;
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
        foreach (var command in _commandsRepository.GetAllCustomCommands())
        {
            _commandResponse[command.Key] = command.Value;
        }

        using var fs = new FileStream("ReservedCommands.txt", FileMode.Open);
        using var sr = new StreamReader(fs);
        foreach (var item in sr.ReadToEnd().Split(','))
        {
            _reservedCommands.Add(item.ToLower());
        }
    }
}