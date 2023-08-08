using Discord.Interactions;

namespace DiscordBot.Commands;

public class GeneralGameModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly Random _rnd = new Random();

    [SlashCommand("flip", "Flips a coin.")]
    public async Task Flip()
    {
        await RespondAsync($"You flipped a {(_rnd.Next(0, 2) == 0 ? "Heads" : "Tails")}.");
    }
}