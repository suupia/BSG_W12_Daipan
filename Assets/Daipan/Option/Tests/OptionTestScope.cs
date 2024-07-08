#nullable enable
using VContainer;
using VContainer.Unity;
using UnityEngine;
using Daipan.Player.Scripts;
using Daipan.Player.Interfaces;

public class OptionTestScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerInput>(Lifetime.Scoped).As<IPlayerInput>();

    }
}