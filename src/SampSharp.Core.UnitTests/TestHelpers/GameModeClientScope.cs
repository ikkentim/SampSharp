using System;

namespace SampSharp.Core.UnitTests.TestHelpers;

public class GameModeClientScope : IDisposable
{
    public GameModeClientScope(IGameModeClient client)
    {
        InternalStorage.RunningClient = client;
    }

    public void Dispose()
    {
        InternalStorage.RunningClient = null;
    }
}