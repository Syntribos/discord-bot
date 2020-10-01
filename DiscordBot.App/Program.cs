using System.Threading;
using System.Threading.Tasks;
using DiscordBot.Config;

namespace DiscordBot.App
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var botConfigFactory = new BotConfigFactory("config.json");
            var bot = new Bot(botConfigFactory, cancellationTokenSource.Token);

            await bot.Initialize();
            bot.RunAsync().GetAwaiter().GetResult();
            cancellationTokenSource.Cancel();
        }
    }
}