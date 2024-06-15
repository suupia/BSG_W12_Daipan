using System.Linq;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Core;
using Daipan.Core.Interfaces;
using Daipan.Core.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.InputSerial.Scripts;
using Daipan.LevelDesign.Battle.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.LevelDesign.Player.Scripts;
using Daipan.LevelDesign.Stream;
using Daipan.LevelDesign.Tower.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Tests;
using Daipan.Streamer.Scripts;
using Daipan.Tower.MonoScripts;
using Daipan.Tower.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Daipan.Daipan

{
    public sealed class DaipanScope : LifetimeScope
    {
        [FormerlySerializedAs("streamParameter")][SerializeField] StreamParam streamParam = null!;
        [SerializeField] PlayerParamManager playerParamManager = null!;
        [FormerlySerializedAs("enemyParamsManager")][SerializeField] EnemyParamManager enemyParamManager = null!;
        [SerializeField] CommentParamsManager commentParamsManager = null!;
        [SerializeField] IrritatedParams irritatedParams = null!;
        [SerializeField] TowerParams towerParams = null!;

        protected override void Configure(IContainerBuilder builder)
        {
            // Domain
            // Stream
            builder.RegisterInstance(streamParam.viewer);
            builder.RegisterInstance(streamParam.daipan);
            builder.RegisterInstance(irritatedParams);
            builder.Register<StreamPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<StreamMono>>();
            builder.Register<IrritatedValue>(Lifetime.Scoped).WithParameter("maxValue", 100);
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
            builder.Register<PlayerAttackEffectPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<PlayerAttackEffectMono>>();
            builder.Register<PlayerAttackEffectSpawner>(Lifetime.Scoped);
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

            builder.Register<EnemyAttackDecider>(Lifetime.Scoped);
            builder.Register<EnemyMonoBuilder>(Lifetime.Scoped).AsImplementedInterfaces()
                .WithParameter(EnemyEnum.RedBoss);
            builder.Register<EnemySpawner>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<EnemyCluster>(Lifetime.Scoped);

            // View
            builder.RegisterComponentInHierarchy<StreamViewMono>();
            builder.RegisterComponentInHierarchy<TimerViewMono>();
            builder.RegisterComponentInHierarchy<StreamerViewMono>();

            // Test
            builder.RegisterComponentInHierarchy<PlayerTestInput>();

            // Parameters
            /*comment*/
            builder.Register<CommentParamsServer>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<CommentPosition>();
            builder.RegisterInstance(commentParamsManager);

            /*enemy*/
            builder.RegisterInstance(enemyParamManager);
            builder.RegisterInstance(enemyParamManager.enemyLevelDesignParam);
            builder.Register<EnemyParamModifyWithTimer>(Lifetime.Scoped);
            builder.RegisterInstance(new EnemyParamDataBuilder(builder, enemyParamManager));
            builder.RegisterInstance(new EnemyLevelDesignParamDataBuilder(builder, enemyParamManager.enemyLevelDesignParam));
            builder.RegisterInstance(new EnemyTimeLineParamDataBuilder(builder, enemyParamManager));

            EnemyPositionMono SetUpEnemyPositionMono()
            {
                var lanePositionMono = Object.FindObjectOfType<LanePositionMono>();
                var enemyPositionMono = new GameObject().AddComponent<EnemyPositionMono>();
                enemyPositionMono.enemySpawnedPoints =
                    lanePositionMono.lanePositions.Select(x => x.enemySpawnedPosition).ToList();
                enemyPositionMono.enemyDespawnedPoint = lanePositionMono.enemyDespawnedPoint;
                return enemyPositionMono;
            }
            builder.RegisterInstance(new EnemyPositionMonoBuilder(builder, SetUpEnemyPositionMono()));


            /*stream*/
            builder.RegisterInstance(new StreamParamDataBuilder(builder, streamParam));

            /*player*/
            PlayerPositionMono SetUpPlayerPositionMono()
            {
                var lanePositionMono = Object.FindObjectOfType<LanePositionMono>();
                var playerPositionMono = new GameObject().AddComponent<PlayerPositionMono>();
                playerPositionMono.playerSpawnedPoint =lanePositionMono.playerSpawnedPosition;
                playerPositionMono.attackEffectDespawnedPoint = lanePositionMono.attackEffectDespawnedPoint;
                return playerPositionMono;
            }
            builder.RegisterInstance(new PlayerPositionMonoBuilder(builder, SetUpPlayerPositionMono()));
            
            builder.RegisterInstance(new PlayerParamDataBuilder(builder, playerParamManager));

            /*tower*/
            builder.Register<TowerParamsConfig>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<TowerPositionMono>();
            builder.RegisterInstance(towerParams);

            // InputSerial
            builder.Register<SerialInput>(Lifetime.Scoped);
            builder.Register<InputSerialManager>(Lifetime.Scoped);

            // Initializer
            builder.RegisterEntryPoint<DaipanInitializer>();

            // Timer
            builder.Register<StreamTimer>(Lifetime.Scoped).AsSelf().As<IStart>().As<IUpdate>();

            // Updater
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints => { entryPoints.Add<Updater>(); });
        }
    }
}
