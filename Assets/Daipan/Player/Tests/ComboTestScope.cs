#nullable enable
using Daipan.Enemy.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;


// プレイヤーの処理をテストするためのLifetimeScope
public class ComboTestScope : LifetimeScope
{


    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<ComboViewMono>();
        builder.Register<ComboCounter>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<ComboTestMono>();
    }
}