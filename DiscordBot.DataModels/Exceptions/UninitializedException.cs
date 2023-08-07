using System.Runtime.CompilerServices;

namespace DiscordBot.DataModels.Exceptions;

public class UninitializedException : Exception
{
    public UninitializedException(Type type, [CallerMemberName] string? method = null)
    : base(method is null 
        ? $"Class {nameof(type)} must be initialized before use."
        : $"Class {nameof(type)} must be initialized before using function {method}.")
    {
        TypeName = nameof(type);
        FunctionName = method;
    }
    
    public string TypeName { get; }
    
    public string? FunctionName { get; }

    public override string ToString() => Message;
}