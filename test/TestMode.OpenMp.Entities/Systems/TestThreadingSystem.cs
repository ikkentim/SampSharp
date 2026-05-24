using SampSharp.Entities;
using SampSharp.OpenMp.Core;

namespace TestMode.OpenMp.Entities.Systems;

public class TestThreadingSystem : ISystem
{
    [Event]
    public void OnGameModeInit()
    {
        Task.Run(async () =>
        {
            Console.WriteLine($"[task] running on main thread? (1) {TaskHelper.IsMainThread()}");
            await Task.Delay(10);
            Console.WriteLine($"[task] running on main thread? (2) {TaskHelper.IsMainThread()}");
            await TaskHelper.SwitchToMainThread();
            Console.WriteLine($"[task] running on main thread? (3) {TaskHelper.IsMainThread()}");
        });

        _ = AsyncVoidTest();
    }

    private static async Task AsyncVoidTest()
    {
        Console.WriteLine($"[async void] running on main thread? (1) {TaskHelper.IsMainThread()}");
        Console.WriteLine("sync context: " + SynchronizationContext.Current);
        await Task.Delay(10);
        Console.WriteLine($"[async void] running on main thread? (2) {TaskHelper.IsMainThread()}");
        Console.WriteLine("sync context: " + SynchronizationContext.Current);
        await TaskHelper.SwitchToMainThread();
        Console.WriteLine($"[async void] running on main thread? (3) {TaskHelper.IsMainThread()}");
        Console.WriteLine("sync context: " + SynchronizationContext.Current);
    }

}