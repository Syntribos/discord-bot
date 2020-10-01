using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Commands;
using DiscordBot.Config;
using DiscordBot.Data;
using DiscordBot.Twitch;
using DiscordBot.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.App
{
    public class Bot
    {
        private readonly BotConfig _botConfig;
        private readonly CancellationToken _token;
        private TwitchClient _twitchClient;
        private StreamerDataRepository _streamerDataRepository;
        private SettingsRepository _settingsRepository;
        private CommandsRepository _commandsRepository;
        private bool _initialized;

        public DiscordSocketClient Client { get; private set; }
        public CommandHandler Commands { get; private set; }

        public Bot(BotConfigFactory botConfigFactory, CancellationToken token)
        {
            _botConfig = botConfigFactory.Create();
            _token = token;
            _initialized = false;
        }

        public async Task Initialize()
        {
            await CreateClient();
            SetupRepositories();
            await CreateAndRegisterCommands();
            CreateBackgroundTasks();
            _initialized = true;
        }

        public async Task RunAsync()
        {
            if (!_initialized)
            {
                throw new NotSupportedException("Bot must be initialized before starting!");
            }

            await Client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task CreateClient()
        {
            Client = new DiscordSocketClient();
            await Client.LoginAsync(Discord.TokenType.Bot, _botConfig.Token);
        }

        private async Task CreateAndRegisterCommands()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(_streamerDataRepository);
            serviceCollection.AddSingleton(_settingsRepository);
            serviceCollection.AddSingleton(_commandsRepository);
            serviceCollection.AddSingleton(_botConfig);
            serviceCollection.AddSingleton(_twitchClient);
            serviceCollection.AddScoped<IAudioService, AudioService>();
            serviceCollection.AddScoped<IYoutubeService, YoutubeService>();
            serviceCollection.AddScoped<IVideoToAudioConverter, VideoToAudioConverter>();

            var commandsServiceConfig = new CommandServiceConfig
            {
                CaseSensitiveCommands = false,

            };
            var commandsService = new CommandService(commandsServiceConfig);
            Commands = new CommandHandler(Client, commandsService, serviceCollection.BuildServiceProvider(), null, _botConfig);

            await Commands.InstallCommandsAsync();
        }

        private void SetupRepositories()
        {
            _streamerDataRepository = new StreamerDataRepository(_botConfig.DatabaseLocation);
            _streamerDataRepository.Initialize();
            _settingsRepository = new SettingsRepository(_botConfig.DatabaseLocation);
            _botConfig.Prefix = _settingsRepository.InitializeAndGetPrefix();
            _commandsRepository = new CommandsRepository(_botConfig.DatabaseLocation);
            _twitchClient = new TwitchClient(_token, _streamerDataRepository);
        }

        private void CreateBackgroundTasks()
        {
            Task.Run(() => _twitchClient.CheckLiveTwitchChannels(TimeSpan.FromSeconds(15)));
        }
    }
}
