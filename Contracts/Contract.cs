using System;

namespace DiscordBot.Contracts
{
    public static class Contract
    {
        public static void RequireNotNull(object testObj, string objName)
        {
            if (testObj.Equals(null))
            {
                throw new ArgumentException($"{objName} cannot be null.");
            }
        }
    }
}
