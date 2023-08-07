using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Utilities
{
    public static class Extensions
    {
        public static string WhiteSpaceToNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
    }
}