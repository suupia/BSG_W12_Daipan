#nullable enable
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Core;
using Daipan.Core.Interfaces;
using Daipan.Daipan;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Player.Scripts;
using Daipan.Stream.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Tests;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class DaipanScope : LifetimeScope
{
    [SerializeField] StreamParameter streamParameter = null!;
    [SerializeField] CommentAttributeParameters commentAttributeParameters = null!;
    [SerializeField] PlayerParameter playerParameter = null!;
    [SerializeField] EnemyAttributeParameters enemyAttributeParameters = null!;
    [SerializeField] CommentParams commentParams = null!;

    protected override void Configure(IContainerBuilder builder)
    {
        // Domain
        // Stream
        builder.RegisterInstance(streamParameter.viewer);
        builder.RegisterInstance(streamParameter.daipan);
        builder.Register<StreamPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<StreamMono>>();
        builder.Register<IrritatedValue>(Lifetime.Scoped).WithParameter(100);
        builder.Register<ViewerNumber>(Lifetime.Scoped);
        builder.Register<StreamStatus>(Lifetime.Scoped);
        builder.Register<IStart, StreamSpawner>(Lifetime.Scoped).AsSelf();

        // Comment
        builder.RegisterInstance(commentAttributeParameters);
        //builder.Register<IStart, CommentSpawnPointContainer>(Lifetime.Scoped).AsSelf();
        builder.Register<CommentPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<CommentMono>>();
        builder.Register<IUpdate,CommentSpawner>(Lifetime.Scoped).AsSelf();
        builder.Register<CommentCluster>(Lifetime.Scoped);


        builder.Register<DaipanExecutor>(Lifetime.Scoped);

        // Player
        builder.RegisterInstance(playerParameter);
        builder.Register<PlayerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<PlayerMono>>();
        builder.Register<PlayerAttack>(Lifetime.Scoped);
        builder.Register<PlayerHolder>(Lifetime.Scoped);
        builder.Register<IStart, PlayerSpawner>(Lifetime.Scoped);

        // Enemy
        builder.RegisterInstance(enemyAttributeParameters);
        builder.Register<EnemyPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<EnemyMono>>();
        builder.Register<EnemyDomainBuilder>(Lifetime.Scoped).As<IEnemyDomainBuilder>();

        builder.Register<EnemyAttack>(Lifetime.Scoped);
        builder.Register<EnemyMonoBuilder>(Lifetime.Scoped).AsImplementedInterfaces()
            .WithParameter(EnemyEnum.Boss);
        builder.Register<EnemySpawner>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
        builder.Register<EnemyCluster>(Lifetime.Scoped);


        // View
        builder.RegisterComponentInHierarchy<StreamViewMono>();
        
        // Test
        builder.RegisterComponentInHierarchy<PlayerTestInput>();

        // Parameters
        /*comment*/
        builder.Register<CommentParamsServer>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<CommentPosition>();
        builder.RegisterInstance(commentParams);


        // Initializer
        builder.RegisterEntryPoint<DaipanInitializer>();


        // Updater
        builder.UseEntryPoints(Lifetime.Scoped, entryPoints => { entryPoints.Add<Updater>(); });
    }
}