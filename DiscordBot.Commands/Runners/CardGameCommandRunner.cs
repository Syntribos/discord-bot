using Discord;
using DiscordBot.CardGames;

namespace DiscordBot.Commands.Runners;

public class CardGameCommandRunner
{
    public async Task DrawCards(IInteractionContext context, int cardCount)
    {
        if (cardCount is < 1 or > 52)
        {
            await context.Interaction.RespondAsync("How many cards do you think there are in a deck, dumbass?");
        }
        var deck = Deck.CreateShuffled52CardDeck();
        await context.Interaction.RespondAsync($"You drew these cards: {string.Join(", ", deck.Draw(cardCount))}");
    }
}