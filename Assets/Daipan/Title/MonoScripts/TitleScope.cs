using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Core;
using Daipan.Core.Interfaces;
using Daipan.Daipan;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.LevelDesign.Tower.Scripts;
using Daipan.Player.Scripts;
using Daipan.Stream.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Tower.Scripts;
using Daipan.Utility.Scripts;
using Daipan.Option.MonoScripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class TitleScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<TitleMono>();

        // Option
        DaipanScope.RegisterOption(builder);

        // Input
        DaipanScope.RegisterInputSerial(builder);
    }
}