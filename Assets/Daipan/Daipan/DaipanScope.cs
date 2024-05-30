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
using Daipan.LevelDesign.Player.Scripts;
using Daipan.Player.Scripts;
using Daipan.Stream.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Tests;
using Daipan.Tower.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class DaipanScope : LifetimeScope
{
    [SerializeField] StreamParameter streamParameter = null!;
    [SerializeField] PlayerParams playerParams = null!;
    [SerializeField] EnemyParamsManager enemyParamsManager = null!;
    [SerializeField] CommentParamsManager commentParamsManager = null!;
    [SerializeField] IrritatedParams irritatedParams = null!;
    [SerializeField] EnemyDefeatParamManager enemyDefeatParamManager = null!;

    protected override void Configure(IContainerBuilder builder)
    {
        // Domain
        // Stream
        builder.RegisterInstance(streamParameter.viewer);
        builder.RegisterInstance(streamParameter.daipan);
        builder.RegisterInstance(irritatedParams);
        builder.Register<StreamPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<StreamMono>>();
        builder.Register<IrritatedValue>(Lifetime.Scoped);
        builder.Register<ViewerNumber>(Lifetime.Scoped);
        builder.Register<StreamStatus>(Lifetime.Scoped);
        builder.Register<IStart, StreamSpawner>(Lifetime.Scoped).AsSelf();

        // Comment
        //builder.RegisterInstance(commentAttributeParameters);
        //builder.Register<IStart, CommentSpawnPointContainer>(Lifetime.Scoped).AsSelf();
        builder.Register<CommentPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<CommentMono>>();
        builder.Register<AntiCommentPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<AntiCommentMono>>();
        builder.Register<IUpdate, CommentSpawner>(Lifetime.Scoped).AsSelf();
        builder.Register<CommentCluster>(Lifetime.Scoped);
        builder.Register<AntiCommentCluster>(Lifetime.Scoped);
        builder.Register<IUpdate, AntiCommentRelocate>(Lifetime.Scoped);

        builder.Register<DaipanExecutor>(Lifetime.Scoped);

        // Player
        //builder.RegisterInstance(playerParameter);
        builder.Register<PlayerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<PlayerMono>>();
        builder.Register<PlayerAttack>(Lifetime.Scoped);
        builder.Register<PlayerHolder>(Lifetime.Scoped);
        builder.Register<IStart, PlayerSpawner>(Lifetime.Scoped);

        // Tower
        builder.Register<TowerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<TowerMono>>();
        builder.Register<IStart, TowerSpawner>(Lifetime.Scoped);

        // Enemy
        //builder.RegisterInstance(enemyAttributeParameters);
        builder.Register<EnemyPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<EnemyMono>>();
        builder.Register<EnemyDomainBuilder>(Lifetime.Scoped).As<IEnemyDomainBuilder>();

        builder.Register<EnemyAttack>(Lifetime.Scoped);
        builder.Register<EnemyMonoBuilder>(Lifetime.Scoped).AsImplementedInterfaces()
            .WithParameter(EnemyEnum.Boss);
        builder.Register<EnemySpawner>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
        builder.Register<EnemyCluster>(Lifetime.Scoped);
        builder.Register<EnemyQuickDefeatChecker>(Lifetime.Scoped);
        builder.Register<EnemySlowDefeatChecker>(Lifetime.Scoped);


        // View
        builder.RegisterComponentInHierarchy<StreamViewMono>();

        // Test
        builder.RegisterComponentInHierarchy<PlayerTestInput>();

        // Parameters
        /*comment*/
        builder.Register<CommentParamsServer>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<CommentPosition>();
        builder.RegisterInstance(commentParamsManager);

        /*enemy*/
        builder.Register<EnemyParamsConfig>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<EnemyPositionMono>();
        builder.RegisterInstance(enemyParamsManager);

        builder.Register<EnemyDefeatConfig>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<EnemyDefeatPositionMono>();
        builder.RegisterInstance(enemyDefeatParamManager);

        /*player*/
        builder.Register<PlayerParamConfig>(Lifetime.Scoped);
        builder.RegisterInstance(playerParams);

        // Initializer
        builder.RegisterEntryPoint<DaipanInitializer>();

        // Timer
        builder.Register<IUpdate, Timer>(Lifetime.Scoped).AsSelf();

        // Updater
        builder.UseEntryPoints(Lifetime.Scoped, entryPoints => { entryPoints.Add<Updater>(); });
    }
}