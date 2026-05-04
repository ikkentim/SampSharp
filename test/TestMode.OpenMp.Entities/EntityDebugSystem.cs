using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.OpenMp.Entities;

public class EntityDebugSystem : ISystem
{
    [Event]
    public void OnConsoleCommandListRequest(ConsoleCommandCollection commands)
    {
        commands.Add("dump");
    }

    [Event]
    public bool OnConsoleText(string command, string args, ConsoleCommandSender sender, IEntityManager entityManager)
    {
        if (command == "dump")
        {
            foreach (var entity in entityManager.GetRootEntities())
            {
                DumpEntities(entityManager, entity, 0);
            }
            return true;
        }

        return false;

    }

    private static void DumpEntities(IEntityManager entityManager, EntityId entity, int depth)
    {
        var ws = string.Concat(Enumerable.Repeat("| ", depth));

        if (depth > 0)
        {
            var ws2 = string.Concat(Enumerable.Repeat("| ", depth - 1));
            Console.WriteLine($"{ws2}+-E: {entity}");
        }
        else
        {
            Console.WriteLine($"E: {entity}");
        }

        foreach (var component in entityManager.GetComponents<Component>(entity))
        {
            Console.WriteLine($"{ws}+C: {component.GetType().Name} ({component})");
        }

        foreach (var child in entityManager.GetChildren(entity))
        {
            DumpEntities(entityManager, child, depth + 1);
        }
    }
}