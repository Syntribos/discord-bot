using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Utilities
{
    public class TempDirectory : IDisposable
    {
        public TempDirectory()
        {
            var tempFileName = Path.GetRandomFileName();
            DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), tempFileName);
            Directory.CreateDirectory(DirectoryPath);
        }

        public void Dispose()
        {
            Directory.Delete(DirectoryPath, true);
        }

        public string DirectoryPath { get; }
    }
}
