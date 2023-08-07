namespace DiscordBot.Utilities
{
    public static class Extensions
    {
        public static string? WhiteSpaceToNull(this string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
    }
}