using System;
using System.Reflection;
using SampSharp.Core;

namespace CommunicationTest
{
    public class GameMode : IGameModeProvider
    {
        #region Implementation of IGameModeProvider

        public void Initialize(IGameModeClient client)
        {
            client.RegisterCallback("OnGameModeInit", this, GetMethod("OnGameModeInit")).Wait();
            client.RegisterCallback("OnPlayerConnected", this, GetMethod("OnPlayerConnected")).Wait();
            client.RegisterCallback("OnPlayerUpdate", this, GetMethod("OnPlayerUpdate")).Wait();
            client.RegisterCallback("OnRconCommand", this, GetMethod("OnRconCommand")).Wait();
        }

        public void Tick()
        {
        }

        #endregion

        private MethodInfo GetMethod(string name)
        {
            return GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        private bool OnGameModeInit()
        {
            Console.WriteLine("[GM] OnGameModeInit");
            return true;
        }

        private bool OnPlayerConnected(int playerid)
        {
            Console.WriteLine($"[GM] OnPlayerConnected {playerid}");
            return true;
        }

        private bool OnPlayerUpdate(int playerid)
        {
            Console.WriteLine($"[GM] OnPlayerUpdate {playerid}");
            return true;
        }

        private bool OnRconCommand(string cmd)
        {
            Console.WriteLine($"[GM] OnRconCommand {cmd}");
            return true;
        }
    }
}