using Discord;
using DiscordBot.Data;
using DiscordBot.Config;
using DiscordBot.Localization;

using Discord.Interactions;
using DiscordBot.Commands.Runners;

namespace DiscordBot.Commands;

public class AdminCommandModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly SettingsRepository _settingsRepository;
    private readonly BotConfig _botConfig;
    private readonly AdminCommandRunner _adminCommandRunner;

    public AdminCommandModule(SettingsRepository settingsRepository, BotConfig botConfig, AdminCommandRunner adminCommandRunner)
    {
        _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
        _botConfig = botConfig ?? throw new ArgumentNullException(nameof(botConfig));
        _adminCommandRunner = adminCommandRunner ?? throw new ArgumentNullException(nameof(adminCommandRunner));
    }
    
    [RequireUserPermission(GuildPermission.BanMembers)]
    [SlashCommand("ban", "Bans the provided user")]
    public async Task BanUser(IUser user, PurgeDays days = PurgeDays.None, string reason = "No reason given.")
    {
        await _adminCommandRunner.BanUser(Context, user, days, reason);
    }

    [SlashCommand("ping", "Checks the bot's current latency.")]
    public async Task Ping()
    {
        await RespondAsync($"Pong! {Context.Client.Latency}ms");
    }

    [SlashCommand("setprefix", "Changes the prefix for the bot's custom commands.")]
    public async Task ChangePrefix(string newPrefix)
    {
        if (string.IsNullOrEmpty(newPrefix))
        {
            await RespondAsync("Your new prefix can't be empty!");
            return;
        }

        if (newPrefix.Length > 2)
        {
            await RespondAsync("Your prefix can't be more than 2 characters!");
            return;
        }

        try
        {
            if (_botConfig.UpdatePrefix(newPrefix))
            {
                
                await RespondAsync($"Your new prefix was changed to {newPrefix}");
            }
            else
            {
                await RespondAsync(Strings.Default_Error_Response);
            }

        }
        catch (Exception e)
        {
            await Console.Error.WriteLineAsync($"Error changing bot prefix: {e.Message}");
            await RespondAsync(Strings.Default_Error_Response);
        }
    }
}
