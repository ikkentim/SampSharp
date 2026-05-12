//HintName: EntryPoint.g.cs
namespace SampSharp
{
    public static class Entrypoint
    {
        private static readonly global::My.Game.Server.Startup _startup = new();
        private static global::SampSharp.OpenMp.Core.StartupContext _context;
        [global::System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute]
        public static void Cleanup()
        {
            _context?.InvokeCleanup();
        }

        [global::System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute]
        public static void Initialize(global::SampSharp.OpenMp.Core.SampSharpInitParams inf)
        {
            _context = new global::SampSharp.OpenMp.Core.StartupContext(inf);
            _context.InitializeUsing(_startup);
        }

        public static void Main()
        {
            SampSharp.OpenMp.Core.StartupContext.MainInfoProvider();
        }
    }
}