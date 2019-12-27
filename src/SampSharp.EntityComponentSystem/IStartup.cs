using System;
using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.EntityComponentSystem
{
    public interface IStartup
    {
        void Configure(IServiceCollection services);
        void Configure(IEcsBuilder builder);
    }
}