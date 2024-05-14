#nullable enable
using Daipan.Enemy.MonoS;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyTestScope : LifetimeScope
{
    [SerializeField] EnemyAttributeParameters enemyAttributeParameters;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(enemyAttributeParameters);
        builder.Register<EnemyPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<EnemyMono>>();

        builder.Register<EnemyAttack>(Lifetime.Scoped);
        builder.Register<EnemyOnHitNormal>(Lifetime.Scoped);
        builder.Register<EnemySpawner>(Lifetime.Scoped);


        builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
        {
            entryPoints.Add<EnemySpawner>();
        });
    }
}