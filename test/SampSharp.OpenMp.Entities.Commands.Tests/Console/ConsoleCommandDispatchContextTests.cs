using System;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Console;

/// <summary>
/// Tests for ConsoleCommandDispatchContext, which provides context and message handling for console commands.
/// </summary>
public class ConsoleCommandDispatchContextTests
{
    [Fact]
    public void Constructor_NoArgs_PlayerIsNull()
    {
        var context = new ConsoleCommandDispatchContext(null);

        context.Player.ShouldBeNull();
    }

    [Fact]
    public void Constructor_NullMessageHandler_IsAllowed()
    {
        var context = new ConsoleCommandDispatchContext(null, null);

        context.MessageHandler.ShouldBeNull();
    }

    [Fact]
    public void Constructor_MessageHandler_IsStored()
    {
        Action<string> handler = _ => { };

        var context = new ConsoleCommandDispatchContext(null, handler);

        context.MessageHandler.ShouldBeSameAs(handler);
    }

    [Fact]
    public void SendMessage_WithHandler_InvokesHandler()
    {
        string? received = null;
        var context = new ConsoleCommandDispatchContext(null, msg => received = msg);

        context.SendMessage("hello world");

        received.ShouldBe("hello world");
    }

    [Fact]
    public void SendMessage_WithNullHandler_DoesNotThrow()
    {
        var context = new ConsoleCommandDispatchContext(null, null);

        Should.NotThrow(() => context.SendMessage("hello"));
    }

    [Fact]
    public void SendMessage_MultipleMessages_EachDelivered()
    {
        var messages = new System.Collections.Generic.List<string>();
        var context = new ConsoleCommandDispatchContext(null, messages.Add);

        context.SendMessage("one");
        context.SendMessage("two");
        context.SendMessage("three");

        messages.ShouldBe(new[] { "one", "two", "three" });
    }
}
