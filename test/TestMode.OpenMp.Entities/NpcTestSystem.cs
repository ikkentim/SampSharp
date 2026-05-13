using System.Numerics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.Commands;

namespace TestMode.OpenMp.Entities;

[CommandGroup("npc")]
public class NpcTestSystem : ISystem
{
    [PlayerCommand("spawn")]
    public void SpawnCommand(Player player, string name, IWorldService worldService)
    {
        var npc = worldService.CreateNpc(name, player);

        npc.Position = player.Position;
        npc.Rotation = player.Rotation;
        npc.Spawn();
    }

    [PlayerCommand("help")]
    public void Help(Player player, IPlayerCommandService commandService, ICommandTextFormatter commandTextFormatter)
    {
        var cmds = commandService.Registry.GetCommandsInGroup(new CommandGroup("npc"));

        player.SendClientMessage("NPC commands:");
        foreach (var cmd in cmds)
        {
            var text = commandTextFormatter.FormatCommandUsage(cmd.Name, cmd.Group.ToString(), cmd.ParsedParameters);
            player.SendClientMessage(text);
        }

        cmds = commandService.Registry.GetCommandsInGroup(new CommandGroup("npc", "path"));
        player.SendClientMessage("NPC path commands:");
        foreach (var cmd in cmds)
        {
            var text = commandTextFormatter.FormatCommandUsage(cmd.Name, cmd.Group.ToString(), cmd.ParsedParameters);
            player.SendClientMessage(text);
        }
    }

    [CommandGroup("path")]
    [PlayerCommand("create")]
    public void PathCreate(Player player, INpcService npcService, float stopRange = 1f)
    {
        var pathId = npcService.CreatePath();
        player.AddComponent<StoredPath>(pathId);
        PathAdd(player, npcService, stopRange);
    }

    [CommandGroup("path")]
    [PlayerCommand("point")]
    public void PathAdd(Player player, INpcService npcService, float stopRange = 1f)
    {
        var path = player.GetComponent<StoredPath>();

        if (path is null)
        {
            player.SendClientMessage("Create path first.");
            return;
        }
        var pathId = path.PathId;
        var success = npcService.AddPointToPath(pathId, player.Position, stopRange);

        if (success)
        {
            player.SendClientMessage("Point added.");
        }
        else
        {
            player.SendClientMessage(Color.Red, "Failed to add point.");
        }
    }

    [CommandGroup("path")]
    [PlayerCommand("record-start")]
    public void PathRecord(Player player, float stopRange = 1, float interval = 1)
    {
        var path = player.GetComponent<StoredPath>();

        if (path is null)
        {
            player.SendClientMessage("Create path first.");
            return;
        }

        if (player.GetComponent<PathRecording>())
        {
            player.SendClientMessage("Recording already started.");
        }
        else
        {
            player.AddComponent<PathRecording>(stopRange, interval);
            player.SendClientMessage("Recording started..");
        }
    }

    [CommandGroup("path")]
    [PlayerCommand("record-stop")]
    public void PathRecordStop(Player player)
    {
        if (player.GetComponent<PathRecording>())
        {
            player.DestroyComponents<PathRecording>();
        }

        player.SendClientMessage("Recording ended.");
    }

    [Timer(1000)]
    public void RecordTimer(IEntityManager en, INpcService npcService)
    {
        foreach (var recording in en.GetComponents<PathRecording>())
        {
            var player = recording.GetComponent<Player>()!;
            var path = recording.GetComponent<StoredPath>()!;

            if ((player.Position - recording.LastPosition).LengthSquared() > recording.IntervalSquared)
            {
                recording.LastPosition = player.Position;
                if (npcService.AddPointToPath(path.PathId, player.Position, recording.StopRange))
                {
                    player.SendClientMessage("Point added.");
                }
                else
                {
                    player.SendClientMessage(Color.Red, "Failed to add point.");
                }
            }
        }
    }


    [CommandGroup("path")]
    [PlayerCommand("destroy")]
    public void PathDestroy(Player player, INpcService npcService)
    {
        var path = player.GetComponent<StoredPath>();

        if (path is null)
        {
            player.SendClientMessage("Create path first.");
            return;
        }
        var pathId = path.PathId;
        npcService.DestroyPath(pathId);
        player.SendClientMessage("Path destroyed.");
    }

    [PlayerCommand("walk-path")]
    public void Ab(Player player)
    {
        var npc = player.GetComponentInChildren<Npc>();

        if (npc is null)
        {
            player.SendClientMessage("Spawn an npc first.");
            return;
        }

        var path = player.GetComponent<StoredPath>();

        if (path is null)
        {
            player.SendClientMessage("Create path first.");
            return;
        }

        npc.MoveByPath(path.PathId);
    }
}

public class StoredPath(int pathId) : Component
{
    public int PathId { get; } = pathId;
}

public class PathRecording(float stopRange, float interval) : Component
{
    public float StopRange { get; } = stopRange;
    public float IntervalSquared { get; } = interval * interval;
    public Vector3 LastPosition { get; set; } = new(float.MinValue);
}