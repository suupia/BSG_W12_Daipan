#nullable enable
using Daipan.Enemy.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;


// プレイヤーの処理をテストするためのLifetimeScope
public class ComboTestScope : LifetimeScope
{

    [SerializeField] ComboParamManager comboParamManager = null!;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<ViewerNumber>(Lifetime.Scoped);

        builder.RegisterInstance(comboParamManager);
        builder.Register<ComboMultiplier>(Lifetime.Scoped).As<IComboMultiplier>();
        builder.Register<ComboCounter>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<ComboViewMono>();
        builder.RegisterComponentInHierarchy<ComboTestMono>();
    }
}