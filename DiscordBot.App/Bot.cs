using Microsoft.Extensions.DependencyInjection;
using System.Data;

using Discord;
using Discord.WebSocket;

using DiscordBot.Commands;
using DiscordBot.Config;
using DiscordBot.Setup;

namespace DiscordBot.App;

public enum BotState
{
    Uninitialized,
    Initializing,
    Initialized,
    Running,
    ShuttingDown,
    ShutDown,
}

public class Bot : IBot
{
    private BotState _state;
    private DiscordSocketClient? _client;
    private CommandHandler? _commands;
    private IServiceProvider? _serviceProvider;

    public Bot()
    {
        _state = BotState.Uninitialized;
    }

    public BotState State => _state;

    public async Task Initialize(IServiceRegistrator serviceRegistrator)
    {
        if (_state != BotState.Uninitialized)
        {
            throw new Exception($"{nameof(Bot)} cannot be initialized from state {_state}.");
        }

        _state = BotState.Initializing;

        serviceRegistrator.RegisterServices();
        _serviceProvider = serviceRegistrator.BuildServices();
        _client = _serviceProvider.GetRequiredService<DiscordSocketClient>();
        SetupClientEvents();

        _commands = _serviceProvider.GetRequiredService<CommandHandler>();
        await _commands.InstallCommandsAsync();
        await _commands.InstallInteractionsAsync();

        _state = BotState.Initialized;
    }

    public async Task Run(BotConfig botConfig)
    {
        if (_state != BotState.Initialized || _client is null)
        {
            throw new Exception($"Client has not been initialized yet. Please call {nameof(Initialize)} before {nameof(Run)}.");
        }

        await _client.LoginAsync(TokenType.Bot, botConfig.Token);
        await _client.StartAsync();

        _state = BotState.Running;
    }

    public async Task Shutdown()
    {
        if (_state != BotState.Running || _client is null)
        {
            throw new Exception($"Client cannot be shut down in this state.");
        }

        _state = BotState.ShuttingDown;
        await _client.LogoutAsync();
        await _client.StopAsync();
        _state = BotState.ShutDown;
    }

    private void SetupClientEvents()
    {
        if (_client == null)
        {
            throw new NoNullAllowedException($"The ${nameof(DiscordSocketClient)} must be instantiated before setting up events.");
        }

        _client.Log += LogAsync;
        _client.Ready += ReadyAsync;
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log);
        return Task.CompletedTask;
    }

    // The Ready event indicates that the client has opened a
    // connection and it is now safe to access the cache.
    private Task ReadyAsync()
    {
        Console.WriteLine($"{_client?.CurrentUser} is connected!");

        return Task.CompletedTask;
    }

    private void CreateBackgroundTasks()
    {
        // Task.Run(() => _twitchClient.CheckLiveTwitchChannels(TimeSpan.FromSeconds(15)));
    }

    public async void Dispose()
    {
        await Shutdown();
    }
}