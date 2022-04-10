using System.IO;
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.Core;

/// <summary>
/// Provides extended functionality for <see cref="GameModeBuilder" /> for configuring a fix for handing RCON commands.
/// </summary>
public static class RconFixGameModeBuilderExtensions
{
    /// <summary>
    /// compiled pawn bytecode of the following code:
    /// <code>
    /// forward OnRconCommand(cmd[]);
    /// public OnRconCommand(cmd[])
    /// {
    ///     return 0;
    /// }
    /// </code>
    /// </summary>
    private static readonly byte[] _rconFix =
    {
        0x59, 0x00, 0x00, 0x00, 0xE0, 0xF1, 0x08, 0x08, 0x04, 0x00, 0x08, 0x00, 0x50, 0x00, 0x00, 0x00, 
        0x68, 0x00, 0x00, 0x00, 0x68, 0x00, 0x00, 0x00, 0x68, 0x40, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 
        0x38, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 
        0x40, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 
        0x1F, 0x00, 0x4F, 0x6E, 0x52, 0x63, 0x6F, 0x6E, 0x43, 0x6F, 0x6D, 0x6D, 0x61, 0x6E, 0x64, 0x00, 
        0x80, 0x78, 0x00, 0x2E, 0x81, 0x09, 0x80, 0x59, 0x30
    };

    /// <summary>
    /// Applies a fix which makes sure custom RCON commands are available. The fix consists of a filterscript being loaded which implements OnRconCommand.
    /// </summary>
    /// <param name="builder">The game mode builder.</param>
    /// <returns>The updated game mode configuration builder.</returns>
    public static GameModeBuilder ApplyRconFix(this GameModeBuilder builder) =>
        builder.AddRunAction((runner, next) =>
        {
            next(runner);

            ApplyRconFix(runner.Client);
        });

    private static void ApplyRconFix(IGameModeClient cli)
    {
        var fsPath = Path.Combine(cli.ServerPath, "filterscripts", "_rconfix.amx");

        if (!File.Exists(fsPath))
        {
            File.WriteAllBytes(fsPath, _rconFix);
        }

        var native = (RconFixNatives)NativeObjectProxyFactory.CreateInstance(typeof(RconFixNatives));
        native.SendRconCommand("loadfs _rconfix");
    }

    /// <summary>
    /// Native functions used for the RCON fix.
    /// </summary>
    public class RconFixNatives
    {
        /// <summary>
        /// Sends an rcon command.
        /// </summary>
        /// <param name="command">The rcon command to send.</param>
        [NativeMethod]
        public virtual void SendRconCommand(string command) => throw new NativeNotImplementedException();
    }
}