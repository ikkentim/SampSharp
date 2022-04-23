using System.Collections;

namespace SampSharp.CommandProcessor;

public class CommandProcessor : ICommandProcessor
{
    private readonly CommandProcessorOptions _options;

    public CommandProcessor(ICommandHelpProvider helpProvider, ICommandUsageProvider usageProvider, CommandProcessorOptions options)
    {
        _options = options;
        HelpProvider = helpProvider;
        UsageProvider = usageProvider;
    }

    private readonly CommandSubProcessor _root = new(null);

    public ICommandHelpProvider HelpProvider { get; }
    public ICommandUsageProvider UsageProvider { get; }

    public bool Run(object? userConext, string commandText)
    {
        var context = new CommandContext(this, _options, userConext);

        var text = commandText.AsSpan();

        if (_options.TrimStart)
        {
            while (!text.IsEmpty && char.IsWhiteSpace(text[0]))
            {
                text = text[1..];
            }
        }

        var parsed = _root.Parse(context, text);

        if (parsed.Success && parsed.Command != null)
        {
            return parsed.Command.Execute(context, parsed);
        }

        return false;
    }
        
    public void AddCommand(ICommandParser command)
    {
        _root.AddCommand(command);
    }

    public IEnumerator<ICommandParser> GetEnumerator()
    {
        return _root.Distinct()
            .GetEnumerator();
    }
        
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}