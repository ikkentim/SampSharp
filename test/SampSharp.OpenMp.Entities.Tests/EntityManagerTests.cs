using SampSharp.Entities;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class EntityManagerTests
{
    // Simple test components
    private class ComponentA : Component { }
    private class ComponentB : Component { }
    private class ComponentC : ComponentA { } // inherits from ComponentA

    private class CallbackComponent : Component
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

    // --- Existence tests ---

    [Fact]
    public void Exists_NewEntity_ReturnsFalse()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.Exists(entity).ShouldBeFalse();
    }

    [Fact]
    public void Exists_AfterAddComponent_ReturnsTrue()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.AddComponent<ComponentA>(entity);
        manager.Exists(entity).ShouldBeTrue();
    }

    [Fact]
    public void Exists_AfterDestroy_ReturnsFalse()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.AddComponent<ComponentA>(entity);
        manager.Destroy(entity);
        manager.Exists(entity).ShouldBeFalse();
    }

    // --- AddComponent / GetComponent tests ---

    [Fact]
    public void AddComponent_ComponentIsRetrievable()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<ComponentA>(entity);
        manager.GetComponent<ComponentA>(entity).ShouldBeSameAs(comp);
    }

    [Fact]
    public void AddComponent_SetsEntityOnComponent()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<ComponentA>(entity);
        comp.Entity.ShouldBe(entity);
    }

    [Fact]
    public void AddComponent_SetsManagerOnComponent()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<ComponentA>(entity);
        comp.Manager.ShouldBeSameAs(manager);
    }

    [Fact]
    public void AddComponent_Instance_ComponentIsRetrievable()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = new ComponentA();
        manager.AddComponent(entity, comp);
        manager.GetComponent<ComponentA>(entity).ShouldBeSameAs(comp);
    }

    [Fact]
    public void AddComponent_CallsOnInitializeComponent()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<CallbackComponent>(entity);
        comp.Initialized.ShouldBeTrue();
    }

    [Fact]
    public void GetComponent_NoComponent_ReturnsNull()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.GetComponent<ComponentA>(entity).ShouldBeNull();
    }

    [Fact]
    public void GetComponent_NonExistentEntity_ReturnsNull()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.GetComponent<ComponentA>(entity).ShouldBeNull();
    }

    [Fact]
    public void GetComponents_MultipleComponents_ReturnsAll()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp1 = manager.AddComponent<ComponentA>(entity);
        var comp2 = manager.AddComponent<ComponentA>(entity);

        var components = manager.GetComponents<ComponentA>(entity);
        components.ShouldContain(comp1);
        components.ShouldContain(comp2);
    }

    [Fact]
    public void GetComponents_NonExistentEntity_ReturnsEmpty()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.GetComponents<ComponentA>(entity).ShouldBeEmpty();
    }

    [Fact]
    public void GetComponent_GlobalSearch_ReturnsAnyComponent()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<ComponentA>(entity);
        manager.GetComponent<ComponentA>().ShouldBeSameAs(comp);
    }

    [Fact]
    public void GetComponents_GlobalSearch_ReturnsAllComponents()
    {
        var manager = CreateManager();
        var entity1 = EntityId.NewEntityId();
        var entity2 = EntityId.NewEntityId();
        var comp1 = manager.AddComponent<ComponentA>(entity1);
        var comp2 = manager.AddComponent<ComponentA>(entity2);

        var all = manager.GetComponents<ComponentA>();
        all.ShouldContain(comp1);
        all.ShouldContain(comp2);
    }

    // --- Destroy tests ---

    [Fact]
    public void Destroy_Entity_RemovesAllComponents()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.AddComponent<ComponentA>(entity);
        manager.AddComponent<ComponentB>(entity);

        manager.Destroy(entity);

        manager.GetComponent<ComponentA>(entity).ShouldBeNull();
        manager.GetComponent<ComponentB>(entity).ShouldBeNull();
    }

    [Fact]
    public void Destroy_Entity_CallsOnDestroyComponent()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<CallbackComponent>(entity);

        manager.Destroy(entity);

        comp.Destroyed.ShouldBeTrue();
    }

    [Fact]
    public void Destroy_ComponentByType_RemovesOnlyThatType()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.AddComponent<ComponentA>(entity);
        var compB = manager.AddComponent<ComponentB>(entity);

        manager.Destroy<ComponentA>(entity);

        manager.GetComponent<ComponentA>(entity).ShouldBeNull();
        manager.GetComponent<ComponentB>(entity).ShouldBeSameAs(compB);
    }

    [Fact]
    public void Destroy_ComponentInstance_RemovesComponent()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<ComponentA>(entity);

        manager.Destroy(comp);

        manager.GetComponent<ComponentA>(entity).ShouldBeNull();
    }

    [Fact]
    public void Destroy_AlreadyDestroyedComponent_DoesNotThrow()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        var comp = manager.AddComponent<ComponentA>(entity);

        manager.Destroy(comp);
        Should.NotThrow(() => manager.Destroy(comp));
    }

    [Fact]
    public void Destroy_NonExistentEntity_DoesNotThrow()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        Should.NotThrow(() => manager.Destroy(entity));
    }

    [Fact]
    public void Destroy_ComponentByType_NonExistentEntity_DoesNotThrow()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        Should.NotThrow(() => manager.Destroy<ComponentA>(entity));
    }

    // --- Parent / child tests ---

    [Fact]
    public void GetParent_NoParent_ReturnsEmpty()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.AddComponent<ComponentA>(entity);
        manager.GetParent(entity).ShouldBe(EntityId.Empty);
    }

    [Fact]
    public void GetParent_WithParent_ReturnsParent()
    {
        var manager = CreateManager();
        var parent = EntityId.NewEntityId();
        var child = EntityId.NewEntityId();

        manager.AddComponent<ComponentA>(parent);
        manager.AddComponent<ComponentA>(child, parent);

        manager.GetParent(child).ShouldBe(parent);
    }

    [Fact]
    public void GetChildren_NoChildren_ReturnsEmpty()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.AddComponent<ComponentA>(entity);
        manager.GetChildren(entity).ShouldBeEmpty();
    }

    [Fact]
    public void GetChildren_WithChildren_ReturnsChildren()
    {
        var manager = CreateManager();
        var parent = EntityId.NewEntityId();
        var child1 = EntityId.NewEntityId();
        var child2 = EntityId.NewEntityId();

        manager.AddComponent<ComponentA>(parent);
        manager.AddComponent<ComponentA>(child1, parent);
        manager.AddComponent<ComponentA>(child2, parent);

        var children = manager.GetChildren(parent);
        children.ShouldContain(child1);
        children.ShouldContain(child2);
    }

    [Fact]
    public void GetChildren_NonExistentEntity_ReturnsEmpty()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.GetChildren(entity).ShouldBeEmpty();
    }

    [Fact]
    public void Destroy_EntityWithChildren_DestroysChildrenToo()
    {
        var manager = CreateManager();
        var parent = EntityId.NewEntityId();
        var child = EntityId.NewEntityId();

        manager.AddComponent<ComponentA>(parent);
        manager.AddComponent<ComponentA>(child, parent);

        manager.Destroy(parent);

        manager.Exists(parent).ShouldBeFalse();
        manager.Exists(child).ShouldBeFalse();
    }

    // --- GetComponentInChildren / GetComponentsInChildren ---

    [Fact]
    public void GetComponentInChildren_ChildHasComponent_ReturnsIt()
    {
        var manager = CreateManager();
        var parent = EntityId.NewEntityId();
        var child = EntityId.NewEntityId();

        manager.AddComponent<ComponentA>(parent);
        var childComp = manager.AddComponent<ComponentB>(child, parent);

        manager.GetComponentInChildren<ComponentB>(parent).ShouldBeSameAs(childComp);
    }

    [Fact]
    public void GetComponentInChildren_NoMatchingChild_ReturnsNull()
    {
        var manager = CreateManager();
        var parent = EntityId.NewEntityId();
        var child = EntityId.NewEntityId();

        manager.AddComponent<ComponentA>(parent);
        manager.AddComponent<ComponentA>(child, parent);

        manager.GetComponentInChildren<ComponentB>(parent).ShouldBeNull();
    }

    [Fact]
    public void GetComponentInChildren_NonExistentEntity_ReturnsNull()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.GetComponentInChildren<ComponentA>(entity).ShouldBeNull();
    }

    [Fact]
    public void GetComponentsInChildren_ReturnsAllChildComponents()
    {
        var manager = CreateManager();
        var parent = EntityId.NewEntityId();
        var child1 = EntityId.NewEntityId();
        var child2 = EntityId.NewEntityId();

        manager.AddComponent<ComponentA>(parent);
        var c1 = manager.AddComponent<ComponentB>(child1, parent);
        var c2 = manager.AddComponent<ComponentB>(child2, parent);

        var results = manager.GetComponentsInChildren<ComponentB>(parent);
        results.ShouldContain(c1);
        results.ShouldContain(c2);
    }

    [Fact]
    public void GetComponentsInChildren_NonExistentEntity_ReturnsEmpty()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.GetComponentsInChildren<ComponentA>(entity).ShouldBeEmpty();
    }

    // --- GetComponentInParent / GetComponentsInParent ---

    [Fact]
    public void GetComponentInParent_ParentHasComponent_ReturnsIt()
    {
        var manager = CreateManager();
        var parent = EntityId.NewEntityId();
        var child = EntityId.NewEntityId();

        var parentComp = manager.AddComponent<ComponentA>(parent);
        manager.AddComponent<ComponentB>(child, parent);

        manager.GetComponentInParent<ComponentA>(child).ShouldBeSameAs(parentComp);
    }

    [Fact]
    public void GetComponentInParent_NoMatchingParent_ReturnsNull()
    {
        var manager = CreateManager();
        var parent = EntityId.NewEntityId();
        var child = EntityId.NewEntityId();

        manager.AddComponent<ComponentA>(parent);
        manager.AddComponent<ComponentA>(child, parent);

        manager.GetComponentInParent<ComponentB>(child).ShouldBeNull();
    }

    [Fact]
    public void GetComponentInParent_NonExistentEntity_ReturnsNull()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.GetComponentInParent<ComponentA>(entity).ShouldBeNull();
    }

    [Fact]
    public void GetComponentsInParent_ReturnsAllParentComponents()
    {
        var manager = CreateManager();
        var grandparent = EntityId.NewEntityId();
        var parent = EntityId.NewEntityId();
        var child = EntityId.NewEntityId();

        var gpComp = manager.AddComponent<ComponentA>(grandparent);
        var pComp = manager.AddComponent<ComponentA>(parent, grandparent);
        manager.AddComponent<ComponentB>(child, parent);

        var results = manager.GetComponentsInParent<ComponentA>(child);
        results.ShouldContain(pComp);
        results.ShouldContain(gpComp);
    }

    // --- GetRootEntities ---

    [Fact]
    public void GetRootEntities_NoEntities_ReturnsEmpty()
    {
        var manager = CreateManager();
        manager.GetRootEntities().ShouldBeEmpty();
    }

    [Fact]
    public void GetRootEntities_RootEntityIncluded()
    {
        var manager = CreateManager();
        var entity = EntityId.NewEntityId();
        manager.AddComponent<ComponentA>(entity);
        manager.GetRootEntities().ShouldContain(entity);
    }

    [Fact]
    public void GetRootEntities_ChildEntity_NotIncluded()
    {
        var manager = CreateManager();
        var parent = EntityId.NewEntityId();
        var child = EntityId.NewEntityId();

        manager.AddComponent<ComponentA>(parent);
        manager.AddComponent<ComponentA>(child, parent);

        var roots = manager.GetRootEntities();
        roots.ShouldContain(parent);
        roots.ShouldNotContain(child);
    }

    // --- Mismatched parent test ---

    [Fact]
    public void AddComponent_MismatchedParent_ThrowsArgumentException()
    {
        var manager = CreateManager();
        var parent1 = EntityId.NewEntityId();
        var parent2 = EntityId.NewEntityId();
        var child = EntityId.NewEntityId();

        manager.AddComponent<ComponentA>(parent1);
        manager.AddComponent<ComponentA>(parent2);
        manager.AddComponent<ComponentA>(child, parent1);

        Should.Throw<ArgumentException>(() => manager.AddComponent<ComponentB>(child, parent2));
    }
}
