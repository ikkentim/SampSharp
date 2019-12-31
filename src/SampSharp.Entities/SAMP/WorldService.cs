using System;
using SampSharp.Entities.SAMP.Components;
using SampSharp.Entities.SAMP.Definitions;
using SampSharp.Entities.SAMP.NativeComponents;
using SampSharp.Entities.SAMP.Systems;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Represents a service for adding entities to the SA:MP world.
    /// </summary>
    public class WorldService : IWorldService
    {
        // TODO: Handling of invalid IDs (SA-MP entity limits)

        private readonly IEntityManager _entityManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldService"/> class.
        /// </summary>
        public WorldService(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        /// <inheritdoc />
        public Actor CreateActor(int modelId, Vector3 position, float rotation)
        {
            var world = _entityManager.Get(WorldSystem.WorldId);

            var id = world.GetComponent<NativeWorld>().CreateActor(modelId, position.X, position.Y, position.Z, rotation);

            var entity = _entityManager.Create(world, SampEntities.GetActorId(id));

            entity.AddComponent<NativeActor>();
            return entity.AddComponent<Actor>();
        }

        /// <inheritdoc />
        public Vehicle CreateVehicle(VehicleModelType type, Vector3 position, float rotation, int color1, int color2,
            int respawnDelay = -1, bool addSiren = false)
        {
            var world = _entityManager.Get(WorldSystem.WorldId);

            var id = world.GetComponent<NativeWorld>().CreateVehicle((int) type, position.X, position.Y, position.Z,
                rotation, color1, color2, respawnDelay, addSiren);

            var entity = _entityManager.Create(world, SampEntities.GetVehicleId(id));

            entity.AddComponent<NativeVehicle>();
            return entity.AddComponent<Vehicle>();
        }

        /// <inheritdoc />
        public Vehicle CreateStaticVehicle(VehicleModelType type, Vector3 position, float rotation, int color1,
            int color2, int respawnDelay = -1, bool addSiren = false)
        {
            var world = _entityManager.Get(WorldSystem.WorldId);

            var id = respawnDelay == -1 && !addSiren
                ? world.GetComponent<NativeWorld>().AddStaticVehicle((int) type, position.X, position.Y, position.Z,
                    rotation, color1, color2)
                : world.GetComponent<NativeWorld>().AddStaticVehicleEx((int) type, position.X, position.Y, position.Z,
                    rotation, color1, color2, respawnDelay, addSiren);

            var entity = _entityManager.Create(world, SampEntities.GetVehicleId(id));

            entity.AddComponent<NativeVehicle>();
            return entity.AddComponent<Vehicle>();
        }
        
        /// <inheritdoc />
        public GangZone CreateGangZone(float minX, float minY, float maxX, float maxY)
        {
            var world = _entityManager.Get(WorldSystem.WorldId);

            var id =  world.GetComponent<NativeWorld>().GangZoneCreate(minX, minY, maxX, maxY);

            var entity = _entityManager.Create(world, SampEntities.GetGangZoneId(id));

            entity.AddComponent<NativeGangZone>();
            return entity.AddComponent<GangZone>(minX, minY, maxX, maxY);
        }
        
        /// <inheritdoc />
        public Pickup CreatePickup(int model, int type, Vector3 position, int virtualWorld = -1)
        {
            var world = _entityManager.Get(WorldSystem.WorldId);

            var id =  world.GetComponent<NativeWorld>().CreatePickup(model, type, position.X, position.Y, position.Z, virtualWorld);

            var entity = _entityManager.Create(world, SampEntities.GetPickupId(id));

            entity.AddComponent<NativePickup>();
            return entity.AddComponent<Pickup>(virtualWorld, model, type, position);
        }
        
        /// <inheritdoc />
        public GlobalObject CreateObject(int modelId, Vector3 position, Vector3 rotation, float drawDistance)
        {
            var world = _entityManager.Get(WorldSystem.WorldId);

            var id =  world.GetComponent<NativeWorld>().CreateObject(modelId, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, drawDistance);
            
            var entity = _entityManager.Create(world, SampEntities.GetObjectId(id));

            entity.AddComponent<NativeObject>();
            return entity.AddComponent<GlobalObject>(drawDistance);
        }
        
        /// <inheritdoc />
        public PlayerObject CreatePlayerObject(Entity player, int modelId, Vector3 position, Vector3 rotation, float drawDistance)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            if(player.GetComponent<NativePlayer>() == null)
                throw new ArgumentException("Entity must be of type player", nameof(player));

            var world = _entityManager.Get(WorldSystem.WorldId);

            var id =  world.GetComponent<NativeWorld>().CreatePlayerObject(player.Id, modelId, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, drawDistance);
            
            var entity = _entityManager.Create(player, SampEntities.GetPlayerObjectId(player.Id, id));

            entity.AddComponent<NativePlayerObject>();
            return entity.AddComponent<PlayerObject>(drawDistance);
        }
        
        /// <inheritdoc />
        public TextLabel CreateTextLabel(string text, Color color, Vector3 position, float drawDistance, int virtualWorld=0, bool testLos = true)
        {
            var world = _entityManager.Get(WorldSystem.WorldId);

            var id =  world.GetComponent<NativeWorld>().Create3DTextLabel(text, color, position.X, position.Y, position.Z, drawDistance, virtualWorld, testLos);
            
            var entity = _entityManager.Create(world, SampEntities.GetTextLabelId(id));

            entity.AddComponent<NativeTextLabel>();
            return entity.AddComponent<TextLabel>(text, color, position, drawDistance, virtualWorld, testLos);
        }
        
        /// <inheritdoc />
        public PlayerTextLabel CreatePlayerTextLabel(Entity player, string text, Color color, Vector3 position, float drawDistance, bool testLos = true, Entity attachedTo = null)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            if(player.GetComponent<NativePlayer>() == null)
                throw new ArgumentException("Entity must be of type player", nameof(player));

            var attachPlayer = NativePlayer.InvalidId;
            var attachVehicle = NativeVehicle.InvalidId;
            
            if (attachedTo?.GetComponent<NativePlayer>() != null)
                attachPlayer = attachedTo.Id;
            else if (attachedTo?.GetComponent<NativeVehicle>() != null)
                attachVehicle = attachedTo.Id;
            else if (attachedTo != null)
                throw new ArgumentException("Attach target must be either a player or a vehicle.", nameof(attachedTo));

            var world = _entityManager.Get(WorldSystem.WorldId);
            
            var id =  world.GetComponent<NativeWorld>().CreatePlayer3DTextLabel(player.Id, text, color, position.X, position.Y, position.Z, drawDistance, attachPlayer, attachVehicle, testLos);

            var entity = _entityManager.Create(player, SampEntities.GetPlayerTextLabelId(player.Id, id));
            
            entity.AddComponent<NativePlayerTextLabel>();
            return entity.AddComponent<PlayerTextLabel>(text, color, position, drawDistance, testLos, attachedTo);
        }
    }
}