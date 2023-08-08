using Microsoft.Extensions.DependencyInjection;

using DiscordBot.Commands.Runners;
using DiscordBot.Config;
using DiscordBot.Data;
using DiscordBot.Commands;
using DiscordBot.DataModels;
using DiscordBot.DataModels.Exceptions;
using DiscordBot.Services;
using DiscordBot.Services.Youtube;

using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using YoutubeDLSharp;
using YouTubeSearch;

namespace DiscordBot.Setup;

public class ServiceRegistrator : IServiceRegistrator
{
    private readonly IServiceCollection _serviceCollection;
    private readonly BotConfig _botConfig;

    private bool _initilialized;

    public ServiceRegistrator(IServiceCollection serviceCollection, BotConfig botConfig)
    {
        _serviceCollection = serviceCollection;
        _botConfig = botConfig;

        _initilialized = false;
    }
    
    public void RegisterServices()
    {
        _serviceCollection
            .RegisterInitializationServices(_botConfig)
            .RegisterDataRepositories()
            .RegisterServices()
            .RegisterCommandServices();

        _initilialized = true;
    }

    public IServiceProvider BuildServices()
    {
        return _initilialized ? _serviceCollection.BuildServiceProvider() : throw new UninitializedException(typeof(ServiceRegistrator));
    }
}

internal static class ServiceExtensions
{
    internal static IServiceCollection RegisterInitializationServices(this IServiceCollection services, BotConfig botConfig)
    {
        return services
            .AddSingleton(botConfig)
            .AddSingleton(BotConfig.SocketConfig)
            .AddSingleton<DiscordSocketClient>();
    }

    internal static IServiceCollection RegisterDataRepositories(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDatabaseInfo, DatabaseInfo>()
            .AddSingleton<CommandsRepository>()
            .AddSingleton<SettingsRepository>()
            .AddSingleton<StreamerDataRepository>();
    }

    internal static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        return services
            .AddScoped<VideoSearch>()
            .AddScoped<YoutubeDL>()
            .AddScoped<IAudioService, AudioService>()
            .AddScoped<IYoutubeService, YoutubeService>()
            .AddScoped<IVideoToAudioConverter, VideoToAudioConverter>();
    }

    internal static IServiceCollection RegisterCommandServices(this IServiceCollection services)
    {
        var commandsServiceConfig = new CommandServiceConfig
        {
            CaseSensitiveCommands = false,
        };

        return services
            .AddSingleton<AdminCommandRunner>()
            .AddSingleton<CardGameRunner>()
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .AddSingleton<ICustomCommandHandler, CustomCommandHandler>()
            .AddSingleton<CommandHandler>()
            .AddSingleton(new CommandService(commandsServiceConfig));
    }
}