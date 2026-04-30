using System.Diagnostics;

namespace SampSharp.OpenMp.Core;

/// <summary>
/// Provides a way to handle exceptions within SampSharp that would otherwise be unhandled.
/// </summary>
public static class SampSharpExceptionHandler
{
    private static ExceptionHandler _exceptionHandler = DefaultExceptionHandler;

    private static void DefaultExceptionHandler(string context, Exception exception)
    {
        Console.WriteLine($"Uncaught exception during {context}:");
        Console.WriteLine(exception.ToString());
    }

    internal static void SetExceptionHandler(ExceptionHandler handler)
    {
        _exceptionHandler = handler;
    }

    /// <summary>
    /// Handles an exception that occurred in the given context.
    /// </summary>
    /// <param name="context">A context which explains where the exception occurred.</param>
    /// <param name="exception">The exception which occured.</param>
    public static void HandleException(string context, Exception exception)
    {
        try
        {
            _exceptionHandler(context, exception);
        }
        catch(Exception ex)
        {
            try
            {
                if (Console.IsOutputRedirected)
                {
                    using var sw = new StreamWriter(Console.OpenStandardOutput());
                    sw.WriteLine($"An exception occurred while handling an exception ({context}):");
                    sw.WriteLine(ex.ToString());
                    sw.WriteLine();
                    sw.WriteLine("Original exception:");
                    sw.WriteLine(exception.ToString());
                }
                else
                {
                    Console.WriteLine($"An exception occurred while handling an exception ({context}):");
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine();
                    Console.WriteLine("Original exception");
                    Console.WriteLine(exception.ToString());
                }
            }
            catch
            {
                // void
            }
        }
        
        if (Debugger.IsAttached)
        {
            Debugger.BreakForUserUnhandledException(exception);
        }
    }
}