using SampSharp.Entities;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.OpenMp.Entities.Systems;

public class TestEntityManagerSystem : ISystem
{
    [ConsoleCommand]
    public void DumpCommand(ConsoleCommandDispatchContext context, IEntityManager entityManager)
    {
        foreach (var entity in entityManager.GetRootEntities())
        {
            DumpEntities(entityManager, entity, 0, str =>
            {
                context.Player?.SendClientMessage(str);
                context.MessageHandler?.Invoke(str);
            });
        }
    }

    private static void DumpEntities(IEntityManager entityManager, EntityId entity, int depth, Action<string> writeLine)
    {
        var ws = string.Concat(Enumerable.Repeat("| ", depth));

        if (depth > 0)
        {
            var ws2 = string.Concat(Enumerable.Repeat("| ", depth - 1));
            writeLine($"{ws2}+-E: {entity}");
        }
        else
        {
            writeLine($"E: {entity}");
        }

        foreach (var component in entityManager.GetComponents<Component>(entity))
        {
            writeLine($"{ws}+C: {component.GetType().Name} ({component})");
        }

        foreach (var child in entityManager.GetChildren(entity))
        {
            DumpEntities(entityManager, child, depth + 1, writeLine);
        }
    }
}