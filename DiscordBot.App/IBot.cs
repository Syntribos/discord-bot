using DiscordBot.Config;
using DiscordBot.Setup;

namespace DiscordBot.App
{
    public interface IBot : IDisposable
    {
        BotState State { get; }

        Task Initialize(IServiceRegistrator serviceRegistrator);

        Task Run(BotConfig botConfig);

        Task Shutdown();
    }
}