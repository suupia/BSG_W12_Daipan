#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class EnemyTestScope : LifetimeScope
{
    [SerializeField] EnemyAttributeParameters enemyAttributeParameters = null!;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(enemyAttributeParameters);
        builder.Register<EnemyPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<EnemyMono>>();

        builder.Register<EnemyAttack>(Lifetime.Scoped);
        builder.Register<EnemyOnHit>(Lifetime.Scoped).As<IEnemyOnHit>();
        builder.Register<EnemySpawner>(Lifetime.Scoped);
        builder.Register<EnemyCluster>(Lifetime.Scoped);


        builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
        {
            entryPoints.Add<EnemySpawner>();
        });
        
    }
}