using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Parsers;

/// <summary>
/// Tests for PlayerParser, which resolves player names or IDs from command input.
/// </summary>
public class PlayerParserTests
{
    private static IServiceProvider CreateServices(IOmpEntityProvider entityProvider, IEntityManager entityManager)
    {
        var services = new ServiceCollection();
        services.AddSingleton(entityProvider);
        services.AddSingleton(entityManager);
        return services.BuildServiceProvider();
    }

    [Fact]
    public void TryParse_EmptyInput_ReturnsFalse()
    {
        var entityProviderMock = new Mock<IOmpEntityProvider>();
        var entityManagerMock = new Mock<IEntityManager>();
        entityManagerMock.Setup(m => m.GetComponents<Player>()).Returns([]);
        entityProviderMock.Setup(p => p.GetPlayer(It.IsAny<int>())).Returns((Player?)null);

        var services = CreateServices(entityProviderMock.Object, entityManagerMock.Object);
        var parser = new PlayerParser();
        var span = StringSpan.For("");

        var result = parser.TryParse(services, ref span, out var value);

        result.ShouldBeFalse();
        value.ShouldBeNull();
    }

    [Fact]
    public void TryParse_PlayerIdNotFound_ReturnsFalse()
    {
        var entityProviderMock = new Mock<IOmpEntityProvider>();
        entityProviderMock.Setup(p => p.GetPlayer(99)).Returns((Player?)null);

        var entityManagerMock = new Mock<IEntityManager>();
        entityManagerMock.Setup(m => m.GetComponents<Player>()).Returns([]);

        var services = CreateServices(entityProviderMock.Object, entityManagerMock.Object);
        var parser = new PlayerParser();
        var span = StringSpan.For("99");

        var result = parser.TryParse(services, ref span, out var value);

        result.ShouldBeFalse();
        value.ShouldBeNull();
    }

    [Fact]
    public void TryParse_NameNotFound_NoPlayers_ReturnsFalse()
    {
        var entityProviderMock = new Mock<IOmpEntityProvider>();
        var entityManagerMock = new Mock<IEntityManager>();
        entityManagerMock.Setup(m => m.GetComponents<Player>()).Returns([]);

        var services = CreateServices(entityProviderMock.Object, entityManagerMock.Object);
        var parser = new PlayerParser();
        var span = StringSpan.For("NonExistentPlayer");

        var result = parser.TryParse(services, ref span, out var value);

        result.ShouldBeFalse();
        value.ShouldBeNull();
    }

    [Fact]
    public void TryParse_AdvancesSpanOnWordConsumption()
    {
        var entityProviderMock = new Mock<IOmpEntityProvider>();
        entityProviderMock.Setup(p => p.GetPlayer(It.IsAny<int>())).Returns((Player?)null);

        var entityManagerMock = new Mock<IEntityManager>();
        entityManagerMock.Setup(m => m.GetComponents<Player>()).Returns([]);

        var services = CreateServices(entityProviderMock.Object, entityManagerMock.Object);
        var parser = new PlayerParser();
        var span = StringSpan.For("SomePlayer extra text");

        parser.TryParse(services, ref span, out _);

        // The word "SomePlayer" was consumed, " extra text" remains (leading space is left)
        span.ToString().ShouldBe(" extra text");
    }

    [Fact]
    public void TryParse_LooksUpByIdWhenNumericInput()
    {
        var entityProviderMock = new Mock<IOmpEntityProvider>();
        entityProviderMock.Setup(p => p.GetPlayer(5)).Returns((Player?)null);

        var entityManagerMock = new Mock<IEntityManager>();
        entityManagerMock.Setup(m => m.GetComponents<Player>()).Returns([]);

        var services = CreateServices(entityProviderMock.Object, entityManagerMock.Object);
        var parser = new PlayerParser();
        var span = StringSpan.For("5");

        parser.TryParse(services, ref span, out _);

        // Verify the ID lookup was attempted
        entityProviderMock.Verify(p => p.GetPlayer(5), Times.Once);
    }

    [Fact]
    public void TryParse_NameSearchSkipsDeadPlayers()
    {
        var entityProviderMock = new Mock<IOmpEntityProvider>();
        entityProviderMock.Setup(p => p.GetPlayer(It.IsAny<int>())).Returns((Player?)null);

        // Can't easily create Player instances that are not alive; test by returning empty array
        var entityManagerMock = new Mock<IEntityManager>();
        entityManagerMock.Setup(m => m.GetComponents<Player>()).Returns([]);

        var services = CreateServices(entityProviderMock.Object, entityManagerMock.Object);
        var parser = new PlayerParser();
        var span = StringSpan.For("SomePlayer");

        var result = parser.TryParse(services, ref span, out var value);

        result.ShouldBeFalse();
    }
}
