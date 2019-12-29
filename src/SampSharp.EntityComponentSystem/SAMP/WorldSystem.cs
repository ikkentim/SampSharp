using System;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.SAMP.NativeComponents;
using SampSharp.EntityComponentSystem.Systems;

namespace SampSharp.EntityComponentSystem.SAMP
{
    public class WorldSystem : IConfiguringSystem
    {
        public static readonly Guid WorldType = new Guid("DD999ED7-9935-4F66-9CDC-E77484AF6BB8");
        public static readonly EntityId WorldId = new EntityId(WorldType, 0);

        public void Configure(IEcsBuilder builder)
        {
            builder.Services.GetService<IEntityManager>()
                .Create(null, WorldId)
                .AddComponent<NativeWorld>();

        }
    }
}