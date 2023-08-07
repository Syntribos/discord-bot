using System.Runtime.CompilerServices;

namespace DiscordBot.DataModels.Exceptions;

public class UninitializedException : Exception
{
    public UninitializedException(Type type, [CallerMemberName] string? method = null)
    {
        TypeName = nameof(type);
        FunctionName = method;
    }

    public string Message => FunctionName is null 
        ? $"Class {TypeName} must be initialized before use."
        : $"Class {TypeName} must be initialized before using function {FunctionName}.";

    public string TypeName { get; }
    
    public string? FunctionName { get; }

    public override string ToString() => Message;
}