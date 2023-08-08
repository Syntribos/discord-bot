using Discord.Interactions;
using DiscordBot.Commands.Runners;

namespace DiscordBot.Commands;

public class GeneralGameModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly GeneralGameRunner _generalGameRunner;

    public GeneralGameModule(GeneralGameRunner generalGameRunner)
    {
        _generalGameRunner = generalGameRunner ?? throw new ArgumentNullException(nameof(generalGameRunner));
    }

    [SlashCommand("flip", "Flips a coin.")]
    public async Task Flip()
    {
        await _generalGameRunner.Flip(Context);
    }
}