using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using SampSharp.Core;
using SampSharp.Core.Natives;

namespace CommunicationTest
{
    public class GameMode : IGameModeProvider
    {
        private IGameModeClient _client;
        #region Implementation of IGameModeProvider

        public void Initialize(IGameModeClient client)
        {
            _client = client;
            client.RegisterCallback("OnGameModeInit", this, GetMethod("OnGameModeInit"));
            client.RegisterCallback("OnPlayerConnect", this, GetMethod("OnPlayerConnect"));
            client.RegisterCallback("OnPlayerUpdate", this, GetMethod("OnPlayerUpdate"));
            client.RegisterCallback("OnRconCommand", this, GetMethod("OnRconCommand"));

            
            TestNativeHandle("SendClientMessage");
            TestNativeHandle("SendClientMessageToAll");
            TestNativeHandle("SendDeathMessage");
            TestNativeHandle("SendDeathMessageToPlayer");
            TestNativeHandle("Foooooooooooooooo");
            TestNativeHandle("Kick");
            TestNativeHandle("Ban");
            TestNativeHandle("GetPlayerName");
            client.Start();

            //PingFire();
        }

        private void TestNativeHandle(string name)
        {
            var handle = _client.GetNativeHandle(name);
            Console.WriteLine($"{name}: {handle}");
        }

        private async void PingFire()
        {
            List<double> pings = new List<double>();
            while (true)
            {
                var ping = await _client.Ping();

                pings.Add(ping.TotalMilliseconds);

                if (pings.Count >= 5000)
                {
                    var count = pings.Count;
                    var orderedPersons = pings.OrderBy(p=>p);
                    var median = orderedPersons.ElementAt(count / 2) + orderedPersons.ElementAt((count - 1) / 2);
                    median /= 2;
                    Console.WriteLine($" pings:{pings.Count} min: {pings.Min()}ms max: {pings.Max()}ms avg: {pings.Average()}ms med:{median}ms");

                    pings.Clear();
                }
                //Console.WriteLine($"Ping is {ping.TotalMilliseconds}ms");
            }
        }

        private DateTime _tickTime;
        private int _ticks = -1;
        public void Tick()
        {
            _ticks++;
            if (_ticks == 0)
            {
                _tickTime = DateTime.Now;
                return;
            }

            if (_tickTime.Second != DateTime.Now.Second)
            {
                Console.WriteLine($"{_ticks} this second.");
                _ticks = 0;
                _tickTime = DateTime.Now;
            }
        }

        #endregion

        private MethodInfo GetMethod(string name)
        {
            return GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        private bool OnGameModeInit()
        {
            Console.WriteLine("[GM] OnGameModeInit");

            var GetNetworkStatsHandle = _client.GetNativeHandle("GetNetworkStats");
            var setWorldTimeHandle = _client.GetNativeHandle("SetWorldTime");
            
            var GetNetworkStats = new Native(_client, GetNetworkStatsHandle, new NativeParameterInfo(NativeParameterType.StringReference, 1), new NativeParameterInfo(NativeParameterType.Int32, 0));

            var setWorldTime = new Native(_client, setWorldTimeHandle, new NativeParameterInfo(NativeParameterType.Int32, 0));

            var args = new object[] { null, 400 };

            GetNetworkStats.Invoke(args);
            Console.WriteLine("netstat " + args[0]);


            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 10; i++)
            {
                GetNetworkStats.Invoke(args);
                setWorldTime.Invoke(i);
            }
            sw.Stop();
            Console.WriteLine($"bench took {sw.Elapsed}");

            return true;
        }

        private bool OnPlayerConnect(int playerid)
        {
            Console.WriteLine($"[GM] OnPlayerConnect {playerid}");

            var getPlayerNameHandle = _client.GetNativeHandle("GetPlayerName");
            var sendClientMessageToAllHandle = _client.GetNativeHandle("SendClientMessageToAll");
            var getPlayerName = new Native(_client, getPlayerNameHandle, new NativeParameterInfo(NativeParameterType.Int32, 0),
                new NativeParameterInfo(NativeParameterType.StringReference, 2), new NativeParameterInfo(NativeParameterType.Int32, 0));

            var sendClientMessageToAll = new Native(_client, sendClientMessageToAllHandle, new NativeParameterInfo(NativeParameterType.Int32, 0),
                new NativeParameterInfo(NativeParameterType.String, 0));

            var args = new object[] { playerid, null, 200 };
            getPlayerName.Invoke(args);
            Console.WriteLine($"Hey y'all! Say hello to {args[1]}!");
            sendClientMessageToAll.Invoke(-1, $"Hey y'all! Say hello to {args[1]}!");

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