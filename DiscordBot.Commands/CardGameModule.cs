using Discord.Interactions;
using DiscordBot.Commands.Runners;

namespace DiscordBot.Commands;

public class CardGameModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly CardGameCommandRunner _cardGameRunner;

    public CardGameModule(CardGameCommandRunner cardGameRunner)
    {
        _cardGameRunner = cardGameRunner ?? throw new ArgumentNullException(nameof(cardGameRunner));
    }

    [SlashCommand("draw", "Draws a random card from a 52 card deck.")]
    public async Task DrawCard(int howMany)
    {
        await _cardGameRunner.DrawCards(Context, howMany);
    }
}