using Discord;

namespace DiscordBot.Commands.Runners;

public enum PurgeDays
{
    None = 0,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
}

public class AdminCommandRunner
{
    public async Task BanUser(IInteractionContext context, IUser user, PurgeDays days, string reason)
    {
        if (await context.Guild.GetUserAsync(user.Id) is not { } guildUser)
        {
            await context.Interaction.RespondAsync($"Couldn't find user {user.Id}");
            return;
        }

        var dayCount = (int)days;

        var builder = new EmbedBuilder()
            .WithAuthor(context.Client.CurrentUser)
            .WithTitle($"Banned user {guildUser.DisplayName} (ID {guildUser.Id})" + (dayCount > 0 ? $" and deleted {dayCount} days of messages." : "."))
            .WithImageUrl(guildUser.GetAvatarUrl())
            .WithColor(Color.Red)
            .WithDescription(reason)
            .WithTimestamp(DateTime.Now);
        var embed = builder.Build();

        await guildUser.BanAsync((int)days, reason);
        await context.Interaction.RespondAsync(embeds: new[] { embed });
    }
}
