// SampSharp
// Copyright 2017 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace CommunicationTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            Process.Start(new ProcessStartInfo
            {
                FileName = @"D:\projects\sampsharp\env\samp-server.exe",
                WorkingDirectory = @"D:\projects\sampsharp\env\"
            });

            Thread.Sleep(1000);
           */
            Console.WriteLine("Connecting.....");

            while (true)
            {
#if CATCHERR
                try
                {
#endif
                using (var stream = new Client())
                {
                    var strs = new Queue<string>();
                    stream.Connect().Wait();

                    Console.WriteLine("Connected!");
                    //                stream.Send(ServerCommand.Ping, null);

                    //                stream.Send(ServerCommand.Print, Client.StringToBytes("Hello, SA-MP streamed world!"));

                    Console.WriteLine("[Client] Registering OnPlayerConnect");
                    stream.Send(ServerCommand.RegisterCall, Client.StringToBytes("OnPlayerConnect")
                            .Concat(new[]
                            {
                                (byte) RegisterCallArguments.Value,
                                (byte) RegisterCallArguments.Terminator
                            }))
                        .Wait();

                    Console.WriteLine("[Client] Registering OnPlayerUpdate");
                    stream.Send(ServerCommand.RegisterCall, Client.StringToBytes("OnPlayerUpdate")
                            .Concat(new[]
                            {
                                (byte) RegisterCallArguments.Value,
                                (byte) RegisterCallArguments.Terminator
                            }))
                        .Wait();

                    Console.WriteLine("[Client] Registering OnRconCommand");
                    stream.Send(ServerCommand.RegisterCall, Client.StringToBytes("OnRconCommand")
                            .Concat(new[]
                            {
                                (byte) RegisterCallArguments.String,
                                (byte) RegisterCallArguments.Terminator
                            }))
                        .Wait();

                    Console.WriteLine("[Client] Getting GetPlayerName handle...");
                    stream.Send(ServerCommand.FindNative, Client.StringToBytes("GetPlayerName")).Wait();
                    strs.Enqueue("GetPlayerName");

                    Console.WriteLine("[Client] Getting GetPlayerIp handle...");
                    stream.Send(ServerCommand.FindNative, Client.StringToBytes("GetPlayerIp")).Wait();
                    strs.Enqueue("GetPlayerIp");

                    Console.WriteLine("[Client] Getting GetPlayerPing handle...");
                    stream.Send(ServerCommand.FindNative, Client.StringToBytes("GetPlayerPing")).Wait();
                    strs.Enqueue("GetPlayerPing");

                    Console.WriteLine("[Client] Getting GetPlayerIp handle...");
                    stream.Send(ServerCommand.FindNative, Client.StringToBytes("GetPlayerIp")).Wait();
                    strs.Enqueue("GetPlayerIp");

                    Console.WriteLine("[Client] Getting DoesNotExisst handle...");
                    stream.Send(ServerCommand.FindNative, Client.StringToBytes("DoesNotExisst")).Wait();
                    strs.Enqueue("DoesNotExisst");

                    Console.WriteLine("[Client] Getting GetNetworkStats handle...");
                    stream.Send(ServerCommand.FindNative, Client.StringToBytes("GetNetworkStats")).Wait();
                    strs.Enqueue("GetNetworkStats");
                    //                stream.Send(ServerCommand.RegisterCall, Client.StringToBytes("OnRconCommand"));
                    //
                    //                stream.Send(ServerCommand.Print, Client.StringToBytes("Registers done."));

                    Console.WriteLine("[Client] Start...");
                    stream.Send(ServerCommand.Start, null).Wait();
                    Console.WriteLine("[Client] Start!");

                    var pings = 0;
                    //stream.Send(ServerCommand.Ping, null).Wait(); //warm up

                    var natives = new Dictionary<string, uint>();
                    var start = DateTime.Now;
                    
                    while (true)
                    {
                        var cmd = stream.Receive().Result;

                        switch (cmd.Command)
                        {
                            case ServerCommand.Pong:
                                if (pings == 0)
                                {
                                    start = DateTime.Now;
                                    stream.Send(ServerCommand.Ping, null).Wait();
                                }
                                else if (pings < 100)
                                {
                                    Console.WriteLine("ping took {0}", DateTime.Now - start);

                                    start = DateTime.Now;
                                    stream.Send(ServerCommand.Ping, null).Wait();
                                }
                                pings++;
                                Console.WriteLine("Pong!");
                                break;
                            case ServerCommand.Tick:
                                Console.WriteLine("Tick!");
                                break;
                            case ServerCommand.PublicCall:
                                var terminatorIndex = Array.IndexOf(cmd.Data, (byte) 0);
                                var pbargs = cmd.Data.Skip(terminatorIndex).ToArray();
                                var call = Encoding.ASCII.GetString(cmd.Data, 0, terminatorIndex);
                                Console.WriteLine($"PUBLIC CALL: {call} {string.Join("", pbargs.Select(n => n.ToString("x2")))}");
                                if (call == "OnGameModeInit")
                                {
                                    stream.Send(ServerCommand.Response, new[] { (byte)1 }.Concat(Client.IntToBytes(1))).Wait(); // return
                                }
                                if (call == "OnPlayerConnect")
                                {
                                    var playerid = Client.BytesToInt(pbargs, 0);
                                    Console.WriteLine("getting name of id " + playerid);

                                    start = DateTime.Now;
//                                    stream.Send(ServerCommand.InvokeNative,
//                                            Client.IntToBytes(natives["GetPlayerName"])
//                                                .Concat(new[] { (byte) RegisterCallArguments.Value })
//                                                .Concat(Client.IntToBytes(playerid))
//                                                .Concat(new[] { (byte) RegisterCallArguments.StringReference })
//                                                .Concat(Client.IntToBytes(40))
//                                                .Concat(new[] { (byte) RegisterCallArguments.Value })
//                                                .Concat(Client.IntToBytes(40))
//                                        )
//                                        .Wait();
                                }
                                else
                                {
                                    Console.WriteLine("sending response");
                                    stream.Send(ServerCommand.Response, new[] { (byte) 1 }.Concat(Client.IntToBytes(1))).Wait(); // return
                                }
                                break;
                            case ServerCommand.Response:
                                if (strs.Count > 0)
                                {
                                    var nat = strs.Dequeue();
                                    Console.WriteLine($"Response! {nat}={Client.BytesToUInt(cmd.Data, 0)}");
                                    natives[nat] = Client.BytesToUInt(cmd.Data, 0);

                                    if (strs.Count == 0)
                                    {
                                        start = DateTime.Now;
                                        stream.Send(ServerCommand.InvokeNative,
                                                Client.IntToBytes(natives["GetNetworkStats"])
                                                    .Concat(new[] { (byte)RegisterCallArguments.StringReference })
                                                    .Concat(Client.IntToBytes(180))
                                                    .Concat(new[] { (byte)RegisterCallArguments.Value })
                                                    .Concat(Client.IntToBytes(180))
                                            )
                                            .Wait();
                                    }
                                }
                                else
                                {
                                    //GetNetworkStats response (first 4 bytes are return value)
                                    var dn = DateTime.Now;
                                    var infoend = Array.IndexOf(cmd.Data, (byte) 0, 4);
                                    var info = Encoding.ASCII.GetString(cmd.Data, 4, infoend - 4);

                                    Console.WriteLine("IN {0}: {1} chars", dn - start, info.Length);


                                    start = DateTime.Now;
                                    stream.Send(ServerCommand.InvokeNative,
                                            Client.IntToBytes(natives["GetNetworkStats"])
                                                .Concat(new[] { (byte)RegisterCallArguments.StringReference })
                                                .Concat(Client.IntToBytes(180))
                                                .Concat(new[] { (byte)RegisterCallArguments.Value })
                                                .Concat(Client.IntToBytes(180))
                                        )
                                        .Wait();

                                    // GetPlayerName response... (first 4 bytes are return value)
                                    //                                    var nameend = Array.IndexOf(cmd.Data, (byte) 0, 4);
                                    //                                    var playername = Encoding.ASCII.GetString(cmd.Data, 4, nameend - 4);
                                    //                                    Console.WriteLine(DateTime.Now - start);
                                    //                                    Console.WriteLine("Name is {0}!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! took that long ^^", playername);
                                    //
                                    //
                                    //                                    Console.WriteLine("sending response");
                                    //                                    stream.Send(ServerCommand.Response, new[] { (byte) 1 }.Concat(Client.IntToBytes(1))).Wait(); // return
                                }
                                break;
                        }
                    }
                }
#if CATCHERR
                }
                catch(Exception e)
                {
                    var j = e;
                    while (j is AggregateException)
                    {
                        j = (j as AggregateException).InnerException;
                    }
                    Console.WriteLine("DEAD... restart {0}", j.Message);
                    Thread.Sleep(1000);
                }
#endif
            }
        }
    }
}