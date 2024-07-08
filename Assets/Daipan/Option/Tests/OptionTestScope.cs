#nullable enable
using VContainer;
using VContainer.Unity;
using UnityEngine;
using Daipan.Player.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Option.Scripts;
using Daipan.Option.Interfaces;

public class OptionTestScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<OptionController>(Lifetime.Scoped).As<IHandleOption>().As<IInputOption>();
        builder.Register<OptionMain>(Lifetime.Scoped).As<IOptionContent>();
    }
}