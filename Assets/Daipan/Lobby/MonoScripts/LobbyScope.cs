#nullable enable

using Fusion;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LobbyScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // [Precondition] 
        var runner = FindObjectOfType<NetworkRunner>();
        if (runner == null) Debug.LogError("NetworkRunner is not found");
        builder.RegisterComponent(runner);

        builder.Register<NetworkPlayerStatsUnitSpawner>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<LobbyInitializerSim>();
    }
}