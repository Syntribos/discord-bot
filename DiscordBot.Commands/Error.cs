using Discord;

namespace DiscordBot.Commands;

public static class Error
{
    private const string NO_EXCEPTION_MESSAGE = "No exception provided.";
    private static readonly TextWriter _errorConsole = Console.Error;

    public static async Task Safely(this IInteractionContext context, Func<Task> task, string errorResponse)
    {
        try
        {
            await task();
        }
        catch (Exception e)
        {
            await context.Interaction.RespondAsync(await WriteError(errorResponse, e));
            throw;
        }
    }
    
    private static async Task<string> WriteError(string response, Exception? exception = null)
    {
        var id = Guid.NewGuid();
        await _errorConsole.WriteLineAsync($"ErrorID: {id} - {exception?.ToString() ?? NO_EXCEPTION_MESSAGE}");
        return $"{response}{Environment.NewLine}Please provide the following ID to the bot creator for more info: {id}";
    }
}