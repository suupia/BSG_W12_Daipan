#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using Enemy;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class DaipanScope : LifetimeScope
{
    [SerializeField] EnemyAttributeParameters enemyAttributeParameters = null!;

    protected override void Configure(IContainerBuilder builder)
    {
        // Domain
        // Stream
        builder.Register<IrritatedValue>(Lifetime.Scoped).WithParameter(100);

        // Enemy
        builder.RegisterInstance(enemyAttributeParameters);
        builder.Register<EnemyPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<EnemyMono>>();

        builder.Register<EnemyAttack>(Lifetime.Scoped);
        builder.Register<EnemyOnHit>(Lifetime.Scoped).As<IEnemyOnHit>();
        builder.Register<EnemySpecificBuilder>(Lifetime.Scoped).AsImplementedInterfaces().WithParameter(EnemyType.A);
        builder.Register<EnemySpawner>(Lifetime.Scoped);
        builder.Register<EnemyCluster>(Lifetime.Scoped);


        // View
        builder.RegisterComponentInHierarchy<StreamViewMono>();


        builder.UseEntryPoints(Lifetime.Scoped, entryPoints => { entryPoints.Add<EnemySpawner>(); });
    }
    
}
