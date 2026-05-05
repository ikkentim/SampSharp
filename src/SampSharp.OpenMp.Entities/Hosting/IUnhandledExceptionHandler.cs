namespace SampSharp.Entities;

public interface IUnhandledExceptionHandler
{
    void Handle(string context, Exception exception);
}