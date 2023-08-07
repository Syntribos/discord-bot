namespace DiscordBot.Setup;

public interface IServiceRegistrator
{
    void RegisterServices();

    IServiceProvider BuildServices();
}