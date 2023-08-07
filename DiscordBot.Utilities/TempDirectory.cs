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
        
        public TempDirectory(string baseDirectory)
        {
            var tempFileName = Path.GetRandomFileName();
            DirectoryPath = Path.Combine(baseDirectory, tempFileName);
            Directory.CreateDirectory(DirectoryPath);
        }

        public void Dispose()
        {
            Directory.Delete(DirectoryPath, true);
            GC.SuppressFinalize(this);
        }

        public string DirectoryPath { get; }
    }
}
