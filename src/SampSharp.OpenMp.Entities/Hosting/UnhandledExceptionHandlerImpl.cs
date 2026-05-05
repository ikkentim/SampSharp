using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

internal class UnhandledExceptionHandlerImpl: IUnhandledExceptionHandler
{
    public void Handle(string context, Exception exception)
    {
        SampSharpExceptionHandler.HandleException(context, exception);
    }
}