using Microsoft.Extensions.DependencyInjection;

using DiscordBot.Config;
using DiscordBot.Setup;

namespace DiscordBot.App;

class Program
{
    private readonly BotConfigFactory _botConfigFactory;

    public Program(string configFilePath)
    {
        _botConfigFactory = new BotConfigFactory(configFilePath);
    }

    public static void Main(string[] _)
    {
        new Program(@"C:\databases\config.json").MainAsync().GetAwaiter().GetResult();
    }

    private async Task MainAsync()
    {
        var botConfig = _botConfigFactory.Create();

        using var bot = new Bot();

        var serviceRegistrator = new ServiceRegistrator(new ServiceCollection(), botConfig);
        await bot.Initialize(serviceRegistrator);
        await bot.Run(botConfig);
        await Task.Delay(Timeout.Infinite);
    }
}