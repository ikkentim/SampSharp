// SampSharp
// Copyright 2020 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Represents a service for adding entities to and control the SA:MP world.
    /// </summary>
    public class WorldService : IWorldService
    {
        /// <summary>
        /// The type of a world entity.
        /// </summary>
        public static readonly Guid WorldType = new Guid("DD999ED7-9935-4F66-9CDC-E77484AF6BB8");

        /// <summary>
        /// The world entity.
        /// </summary>
        public static readonly EntityId World = new EntityId(WorldType, 0);

        private readonly IEntityManager _entityManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldService" /> class.
        /// </summary>
        public WorldService(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        private NativeWorld Native => _entityManager.GetComponent<NativeWorld>(World);

        /// <inheritdoc />
        public float Gravity
        {
            get => Native.GetGravity();
            set
            {
                if (value < -50 || value > 50)
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                        "Value must be between -50.0 and 50.0.");

                Native.SetGravity(value);
            }
        }

        /// <inheritdoc />
        public Actor CreateActor(int modelId, Vector3 position, float rotation)
        {
            var id = Native
                .CreateActor(modelId, position.X, position.Y, position.Z, rotation);

            if (id == NativeActor.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetActorId(id);
            _entityManager.Create(entity, World);

            _entityManager.AddComponent<NativeActor>(entity);
            return _entityManager.AddComponent<Actor>(entity);
        }

        /// <inheritdoc />
        public Vehicle CreateVehicle(VehicleModelType type, Vector3 position, float rotation, int color1, int color2,
            int respawnDelay = -1, bool addSiren = false)
        {
            var id = Native.CreateVehicle((int) type, position.X, position.Y, position.Z,
                rotation, color1, color2, respawnDelay, addSiren);
            
            if (id == NativeVehicle.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetVehicleId(id);
            _entityManager.Create(entity, World);

            _entityManager.AddComponent<NativeVehicle>(entity);
            return _entityManager.AddComponent<Vehicle>(entity);
        }

        /// <inheritdoc />
        public Vehicle CreateStaticVehicle(VehicleModelType type, Vector3 position, float rotation, int color1,
            int color2, int respawnDelay = -1, bool addSiren = false)
        {
            var id = respawnDelay == -1 && !addSiren
                ? Native.AddStaticVehicle((int) type, position.X, position.Y, position.Z,
                    rotation, color1, color2)
                : Native.AddStaticVehicleEx((int) type, position.X, position.Y, position.Z,
                    rotation, color1, color2, respawnDelay, addSiren);
            
            if (id == NativeVehicle.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetVehicleId(id);
            _entityManager.Create(entity, World);

            _entityManager.AddComponent<NativeVehicle>(entity);
            return _entityManager.AddComponent<Vehicle>(entity);
        }

        /// <inheritdoc />
        public GangZone CreateGangZone(float minX, float minY, float maxX, float maxY)
        {
            var id = Native.GangZoneCreate(minX, minY, maxX, maxY);

            if (id == NativeGangZone.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetGangZoneId(id);
            _entityManager.Create(entity, World);
            
            _entityManager.AddComponent<NativeGangZone>(entity);
            return _entityManager.AddComponent<GangZone>(entity, minX, minY, maxX, maxY);
        }

        /// <inheritdoc />
        public Pickup CreatePickup(int model, int type, Vector3 position, int virtualWorld = -1)
        {
            var id = _entityManager.GetComponent<NativeWorld>()
                .CreatePickup(model, type, position.X, position.Y, position.Z, virtualWorld);
            
            if (id == NativePickup.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetPickupId(id);
            _entityManager.Create(entity, World);

            _entityManager.AddComponent<NativePickup>(entity);
            return _entityManager.AddComponent<Pickup>(entity, virtualWorld, model, type, position);
        }

        /// <inheritdoc />
        public bool AddStaticPickup(int model, int type, Vector3 position, int virtualWorld = -1)
        {
            return Native.AddStaticPickup(model, type, position.X, position.Y, position.Z, virtualWorld);
        }

        /// <inheritdoc />
        public GlobalObject CreateObject(int modelId, Vector3 position, Vector3 rotation, float drawDistance)
        {
            var id = Native.CreateObject(modelId, position.X, position.Y, position.Z,
                rotation.X, rotation.Y, rotation.Z, drawDistance);
            
            if (id == NativeObject.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetObjectId(id);
            _entityManager.Create(entity, World);

            _entityManager.AddComponent<NativeObject>(entity);
            return _entityManager.AddComponent<GlobalObject>(entity, drawDistance);
        }

        /// <inheritdoc />
        public PlayerObject CreatePlayerObject(EntityId player, int modelId, Vector3 position, Vector3 rotation,
            float drawDistance)
        {
            if (!player.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            var id = Native.CreatePlayerObject(player, modelId, position.X, position.Y,
                position.Z, rotation.X, rotation.Y, rotation.Z, drawDistance);
            
            if (id == NativePlayerObject.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetPlayerObjectId(player, id);
            _entityManager.Create(entity, player);

            _entityManager.AddComponent<NativePlayerObject>(entity);
            return _entityManager.AddComponent<PlayerObject>(entity, drawDistance);
        }

        /// <inheritdoc />
        public TextLabel CreateTextLabel(string text, Color color, Vector3 position, float drawDistance,
            int virtualWorld = 0, bool testLos = true)
        {
            var id = Native.Create3DTextLabel(text, color, position.X, position.Y,
                position.Z, drawDistance, virtualWorld, testLos);
            
            if (id == NativeTextLabel.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetTextLabelId(id);
            _entityManager.Create(entity, World);

            _entityManager.AddComponent<NativeTextLabel>(entity);
            return _entityManager.AddComponent<TextLabel>(entity, text, color, position, drawDistance, virtualWorld, testLos);
        }

        /// <inheritdoc />
        public PlayerTextLabel CreatePlayerTextLabel(EntityId player, string text, Color color, Vector3 position,
            float drawDistance, bool testLos = true, EntityId attachedTo = default)
        {
            if (!player.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            var attachPlayer = NativePlayer.InvalidId;
            var attachVehicle = NativeVehicle.InvalidId;

            if (attachedTo)
            {
                if (!player.IsOfAnyType(SampEntities.PlayerType, SampEntities.VehicleType))
                    throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType,
                        SampEntities.VehicleType);

                if (attachedTo.IsOfType(SampEntities.PlayerType))
                    attachPlayer = attachedTo;
                else
                    attachVehicle = attachedTo;
            }

            var id = Native.CreatePlayer3DTextLabel(player, text, color, position.X,
                position.Y, position.Z, drawDistance, attachPlayer, attachVehicle, testLos);
            
            if (id == NativePlayerTextLabel.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetPlayerTextLabelId(player, id);
            _entityManager.Create(entity, player);

            _entityManager.AddComponent<NativePlayerTextLabel>(entity);
            return _entityManager.AddComponent<PlayerTextLabel>(entity, text, color, position, drawDistance, testLos, attachedTo);
        }

        /// <inheritdoc />
        public TextDraw CreateTextDraw(Vector2 position, string text)
        {
            var id = Native.TextDrawCreate(position.X, position.Y, string.IsNullOrEmpty(text) ? "_" : text);
            
            if (id == NativeTextDraw.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetTextDrawId(id);
            _entityManager.Create(entity, World);

            _entityManager.AddComponent<NativeTextDraw>(entity);
            return _entityManager.AddComponent<TextDraw>(entity, position, text);
        }
        
        /// <inheritdoc />
        public PlayerTextDraw CreatePlayerTextDraw(EntityId player, Vector2 position, string text)
        {
            if (!player.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            var id = Native.CreatePlayerTextDraw(player, position.X, position.Y, string.IsNullOrEmpty(text) ? "_" : text);
            
            if (id == NativePlayerTextDraw.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetPlayerTextDrawId(player, id);
            _entityManager.Create(entity, player);

            _entityManager.AddComponent<NativePlayerTextDraw>(entity);
            return _entityManager.AddComponent<PlayerTextDraw>(entity, position, text);
        }
        
        /// <inheritdoc />
        public Menu CreateMenu(string title, Vector2 position, float col0Width, float? col1Width = null)
        {
            var columns = col1Width != null ? 2 : 1;

            var id = Native.CreateMenu(title, columns, position.X, position.Y, col0Width, col1Width ?? 0.0f);
            
            if (id == NativeMenu.InvalidId)
                throw new EntityCreationException();

            var entity = SampEntities.GetMenuId(id);
            _entityManager.Create(entity, World);

            _entityManager.AddComponent<NativeMenu>(entity);
            return _entityManager.AddComponent<Menu>(entity, title, columns, position, col0Width, col1Width ?? 0.0f);
        }

        /// <inheritdoc />
        public void SetObjectsDefaultCameraCollision(bool disable)
        {
            Native.SetObjectsDefaultCameraCol(disable);
        }

        /// <inheritdoc />
        public void SendClientMessage(Color color, string message)
        {
            Native.SendClientMessageToAll(color, message);
        }

        /// <inheritdoc />
        public void SendClientMessage(Color color, string messageFormat, params object[] args)
        {
            SendClientMessage(color, string.Format(messageFormat, args));
        }

        /// <inheritdoc />
        public void SendClientMessage(string message)
        {
            SendClientMessage(Color.White, message);
        }

        /// <inheritdoc />
        public void SendClientMessage(string messageFormat, params object[] args)
        {
            SendClientMessage(Color.White, string.Format(messageFormat, args));
        }

        /// <inheritdoc />
        public void SendPlayerMessageToPlayer(EntityId sender, string message)
        {
            if (!sender.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(sender), SampEntities.PlayerType);

            Native.SendPlayerMessageToAll(sender, message);
        }

        /// <inheritdoc />
        public void SendDeathMessage(EntityId killer, EntityId player, Weapon weapon)
        {
            if (killer && !killer.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(killer), SampEntities.PlayerType);

            if (!player.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            Native.SendDeathMessage(killer.OrElse(NativePlayer.InvalidId), player, (int) weapon);
        }

        /// <inheritdoc />
        public void GameText(string text, int time, int style)
        {
            Native.GameTextForAll(text, time, style);
        }

        /// <inheritdoc />
        public void CreateExplosion(Vector3 position, ExplosionType type, float radius)
        {
            Native.CreateExplosion(position.X, position.Y, position.Z, (int) type, radius);
        }

        /// <inheritdoc />
        public void SetWeather(int weather)
        {
            Native.SetWeather(weather);
        }
    }
}