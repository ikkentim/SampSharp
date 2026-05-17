using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Commands.Tests.Services;

public class PlayerCommandServiceTests
{
    private sealed class AdminComponent : Component;
    private sealed class NullableParameterCommandSystem : ISystem
    {
        public static int? LastMoney { get; private set; }

        [PlayerCommand]
        public void Money(Player player, int? money = null)
        {
            LastMoney = money;
        }

        public static void Reset()
        {
            LastMoney = null;
        }
    }

    private sealed class ComponentRestrictedCommandSystem : ISystem
    {
        [PlayerCommand]
        public void Test(AdminComponent player)
        {
        }
    }
    [Fact]
    public void Invoke_ComponentMismatch_SendsPermissionDenied()
    {
        var playerEntity = EntityId.NewEntityId();
        var playerComponent = (SampSharp.Entities.SAMP.Player)RuntimeHelpers.GetUninitializedObject(typeof(SampSharp.Entities.SAMP.Player));

        var entityManager = new Mock<IEntityManager>();
        entityManager.Setup(m => m.GetComponent<SampSharp.Entities.SAMP.Player>(playerEntity)).Returns(playerComponent);
        entityManager.Setup(m => m.GetComponent<AdminComponent>(playerEntity)).Returns((AdminComponent?)null);

        var systemRegistry = new Mock<ISystemRegistry>();
        systemRegistry.Setup(r => r.GetSystemTypes()).Returns(new ReadOnlyMemory<Type>([typeof(ComponentRestrictedCommandSystem)]));

        var messageService = new Mock<IPlayerCommandMessageService>();
        messageService.Setup(m => m.SendPermissionDenied(playerComponent, It.IsAny<CommandDefinition>())).Returns(true);

        var services = new Mock<IServiceProvider>();
        services.Setup(s => s.GetService(typeof(IEntityManager))).Returns(entityManager.Object);

        var service = new PlayerCommandService(
            entityManager.Object,
            systemRegistry.Object,
            messageService.Object,
            new Mock<IPermissionChecker>().Object,
            new Mock<IUnhandledExceptionHandler>().Object,
            new DefaultCommandParameterParserFactory(),
            Options.Create(new PlayerCommandServiceOptions()),
            NullLoggerFactory.Instance);

        var result = service.Invoke(services.Object, playerEntity, "/test");

        result.ShouldBeTrue();
        messageService.Verify(m => m.SendPermissionDenied(playerComponent, It.IsAny<CommandDefinition>()), Times.Once);
        messageService.Verify(m => m.SendCommandNotFound(It.IsAny<SampSharp.Entities.SAMP.Player>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void Invoke_NullableIntArgument_ExecutesWithParsedValue()
    {
        NullableParameterCommandSystem.Reset();
        var (service, services, playerEntity) = CreateNullableCommandService();
        var result = service.Invoke(services, playerEntity, "/money 123");

        result.ShouldBeTrue();
        NullableParameterCommandSystem.LastMoney.ShouldBe(123);
    }

    [Fact]
    public void Invoke_NullableIntArgumentMissing_ExecutesWithNullDefault()
    {
        NullableParameterCommandSystem.Reset();
        var (service, services, playerEntity) = CreateNullableCommandService();
        var result = service.Invoke(services, playerEntity, "/money");

        result.ShouldBeTrue();
        NullableParameterCommandSystem.LastMoney.ShouldBeNull();
    }

    private static (PlayerCommandService service, IServiceProvider services, EntityId playerEntity) CreateNullableCommandService()
    {
        var playerEntity = EntityId.NewEntityId();
        var playerComponent = (SampSharp.Entities.SAMP.Player)RuntimeHelpers.GetUninitializedObject(typeof(SampSharp.Entities.SAMP.Player));

        var entityManager = new Mock<IEntityManager>();
        entityManager.Setup(m => m.GetComponent<SampSharp.Entities.SAMP.Player>(playerEntity)).Returns(playerComponent);

        var systemRegistry = new Mock<ISystemRegistry>();
        systemRegistry.Setup(r => r.GetSystemTypes()).Returns(new ReadOnlyMemory<Type>([typeof(NullableParameterCommandSystem)]));

        var messageService = new Mock<IPlayerCommandMessageService>();

        var permissionChecker = new Mock<IPermissionChecker>();
        permissionChecker.Setup(p => p.HasPermission(playerComponent, It.IsAny<CommandDefinition>())).Returns(true);

        var services = new Mock<IServiceProvider>();
        services.Setup(s => s.GetService(typeof(IEntityManager))).Returns(entityManager.Object);
        services.Setup(s => s.GetService(typeof(NullableParameterCommandSystem))).Returns(new NullableParameterCommandSystem());

        var service = new PlayerCommandService(
            entityManager.Object,
            systemRegistry.Object,
            messageService.Object,
            permissionChecker.Object,
            new Mock<IUnhandledExceptionHandler>().Object,
            new DefaultCommandParameterParserFactory(),
            Options.Create(new PlayerCommandServiceOptions()),
            NullLoggerFactory.Instance);

        return (service, services.Object, playerEntity);
    }
}
