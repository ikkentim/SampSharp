namespace SampSharp.CommandProcessor;

public interface ICommandProcessor : ICommandCollection
{
    ICommandHelpProvider HelpProvider { get; }
    ICommandUsageProvider UsageProvider { get; }
    bool Run(object userConext, string commandText);
    void AddCommand(ICommandParser command);
}