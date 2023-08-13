namespace DiscordBot.Utilities
{
    public static class Extensions
    {
        private static readonly Random _rng = new Random();  

        public static string? WhiteSpaceToNull(this string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public static void Shuffle<T>(this IList<T> list)  
        {  
            var n = list.Count;  
            while (n > 1) {  
                n--;  
                var k = _rng.Next(n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }  
        }
        
        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : string.Concat(value.AsSpan(0, maxChars), "…");
        }
    }
}