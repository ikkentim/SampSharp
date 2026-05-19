using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace Company.GameMode;

public class MyFirstSystem : ISystem
{
    [Event]
    public void OnPlayerConnect(Player player)
    {
        var connectInfo = player.AddComponent<MyFirstComponent>();
        connectInfo.ConnectedAtUtc = DateTime.UtcNow;

        player.SendClientMessage("Welcome to your first SampSharp ECS game mode!");
    }

    [PlayerCommand("connect-time")]
    public void ConnectTime(Player player)
    {
        var connectInfo = player.GetComponent<MyFirstComponent>();

        if (connectInfo == null)
        {
            player.SendClientMessage("Your connect time is not available yet.");
            return;
        }

        player.SendClientMessage($"You connected at {connectInfo.ConnectedAtUtc:yyyy-MM-dd HH:mm:ss} UTC.");
    }
}
