using SampSharp.Entities;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class ComponentTests
{
    private class SimpleComponent : Component { }

    private class ChildComponent : Component { }

    private class LifecycleComponent : Component
    {
        public bool Initialized { get; private set; }
        public bool Destroyed { get; private set; }

        protected override void OnInitializeComponent()
        {
            Initialized = true;
        }

        protected override void OnDestroyComponent()
        {
            Destroyed = true;
        }
    }

    private static EntityManager CreateManager() => new();

    [Fact]
    public void IsComponentAlive_AfterAdd_IsTrue()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);

        comp.IsComponentAlive.ShouldBeTrue();
    }

    [Fact]
    public void IsComponentAlive_AfterDestroy_IsFalse()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);

        manager.Destroy(comp);

        comp.IsComponentAlive.ShouldBeFalse();
    }

    [Fact]
    public void IsDestroying_AfterDestroy_IsTrue()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);

        manager.Destroy(comp);

        comp.IsDestroying.ShouldBeTrue();
    }

    [Fact]
    public void OperatorTrue_AliveComponent_ReturnsTrue()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);

        (comp ? true : false).ShouldBeTrue();
    }

    [Fact]
    public void OperatorTrue_DestroyedComponent_ReturnsFalse()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);
        manager.Destroy(comp);

        (comp ? true : false).ShouldBeFalse();
    }

    [Fact]
    public void OperatorTrue_NullComponent_ReturnsFalse()
    {
        SimpleComponent? comp = null;
        (comp ? true : false).ShouldBeFalse();
    }

    [Fact]
    public void OperatorNot_AliveComponent_ReturnsFalse()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);

        (!comp).ShouldBeFalse();
    }

    [Fact]
    public void OperatorNot_DestroyedComponent_ReturnsTrue()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);
        manager.Destroy(comp);

        (!comp).ShouldBeTrue();
    }

    [Fact]
    public void OperatorNot_NullComponent_ReturnsTrue()
    {
        SimpleComponent? comp = null;
        (!comp).ShouldBeTrue();
    }

    [Fact]
    public void Entity_ReturnsCorrectEntityId()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);

        comp.Entity.ShouldBe(entity);
    }

    [Fact]
    public void ImplicitConversionToEntityId_ReturnsComponentEntity()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);

        EntityId entityId = comp;
        entityId.ShouldBe(entity);
    }

    [Fact]
    public void ImplicitConversionToEntityId_NullComponent_ReturnsEmpty()
    {
        SimpleComponent? comp = null;
        EntityId entityId = comp!;
        entityId.ShouldBe(EntityId.Empty);
    }

    [Fact]
    public void GetComponent_ReturnsComponentOnSameEntity()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp1 = manager.AddComponent<SimpleComponent>(entity);
        var comp2 = manager.AddComponent<ChildComponent>(entity);

        comp1.GetComponent<ChildComponent>().ShouldBeSameAs(comp2);
    }

    [Fact]
    public void AddComponent_AddsNewComponentToSameEntity()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);

        var child = comp.AddComponent<ChildComponent>();

        manager.GetComponent<ChildComponent>(entity).ShouldBeSameAs(child);
    }

    [Fact]
    public void Destroy_RemovesSelf()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);

        comp.Destroy();

        manager.GetComponent<SimpleComponent>(entity).ShouldBeNull();
    }

    [Fact]
    public void DestroyEntity_RemovesEntity()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);

        comp.DestroyEntity();

        manager.Exists(entity).ShouldBeFalse();
    }

    [Fact]
    public void DestroyComponents_RemovesAllOfType()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<SimpleComponent>(entity);
        var child = manager.AddComponent<ChildComponent>(entity);

        comp.DestroyComponents<ChildComponent>();

        manager.GetComponent<ChildComponent>(entity).ShouldBeNull();
        manager.GetComponent<SimpleComponent>(entity).ShouldBeSameAs(comp);
    }

    [Fact]
    public void OnInitializeComponent_CalledOnAdd()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<LifecycleComponent>(entity);

        comp.Initialized.ShouldBeTrue();
    }

    [Fact]
    public void OnDestroyComponent_CalledOnDestroy()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<LifecycleComponent>(entity);

        manager.Destroy(comp);

        comp.Destroyed.ShouldBeTrue();
    }

    [Fact]
    public void Manager_NotYetAdded_ThrowsInvalidOperationException()
    {
        var comp = new SimpleComponentWithPublicAccess();
        Should.Throw<InvalidOperationException>(() => _ = comp.Manager);
    }

    private class SimpleComponentWithPublicAccess : Component
    {
        public new IEntityManager Manager => base.Manager;
    }
}
