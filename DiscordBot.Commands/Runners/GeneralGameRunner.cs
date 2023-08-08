using Discord;

namespace DiscordBot.Commands.Runners;

public class GeneralGameRunner
{
    private readonly Random _rnd = new Random();

    public async Task Flip(IInteractionContext context)
    {
        await context.Interaction.RespondAsync($"You flipped a {(_rnd.Next(0, 2) == 0 ? "Heads" : "Tails")}.");
    }
}