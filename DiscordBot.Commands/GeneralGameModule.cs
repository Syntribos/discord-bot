using Discord.Interactions;
using DiscordBot.Commands.Runners;

namespace DiscordBot.Commands;

public class GeneralGameModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly GeneralGameRunner _gameRunner;

    public GeneralGameModule(GeneralGameRunner gameRunner)
    {
        _gameRunner = gameRunner ?? throw new ArgumentNullException(nameof(gameRunner));
    }

    [SlashCommand("flip", "Flips a coin.")]
    public async Task Flip()
    {
        await _gameRunner.Flip(Context);
    }
}