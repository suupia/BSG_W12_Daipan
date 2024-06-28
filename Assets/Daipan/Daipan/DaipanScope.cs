using System.Linq;
using Daipan.Battle.interfaces;
using Daipan.Battle.scripts;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Core.Interfaces;
using Daipan.Core.Scripts;
using Daipan.DebugInput.MonoScripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.InputSerial.Scripts;
using Daipan.LevelDesign.Battle.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.LevelDesign.Stream;
using Daipan.LevelDesign.Tower.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
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
        [FormerlySerializedAs("streamParameter")] [SerializeField]
        StreamParam streamParam = null!;

        [SerializeField] PlayerParamManager playerParamManager = null!;

        [FormerlySerializedAs("enemyParamsManager")] [SerializeField]
        EnemyParamManager enemyParamManager = null!;

        [FormerlySerializedAs("commentParamsManager")] [SerializeField] CommentParamManager commentParamManager = null!;
        [SerializeField] IrritatedParams irritatedParams = null!;
        [SerializeField] TowerParams towerParams = null!;
        [SerializeField] ComboParamManager comboParamManager = null!;

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
            builder.Register<CommentPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<CommentMono>>();
            builder.Register<AntiCommentPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<AntiCommentMono>>();
            builder.Register<IUpdate, CommentSpawner>(Lifetime.Scoped).AsSelf();
            builder.Register<CommentCluster>(Lifetime.Scoped);
            builder.Register<AntiCommentCluster>(Lifetime.Scoped);
            builder.Register<IUpdate, AntiCommentRelocate>(Lifetime.Scoped);

            builder.Register<DaipanExecutor>(Lifetime.Scoped);

            // Player
            builder.Register<PlayerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<PlayerMono>>();
            builder.Register<PlayerAttackEffectPrefabLoader>(Lifetime.Scoped)
                .As<IPrefabLoader<PlayerAttackEffectMono>>();
            builder.Register<PlayerAttackEffectSpawner>(Lifetime.Scoped);
            builder.Register<PlayerAttackEffectBuilder>(Lifetime.Scoped);
            builder.Register<PlayerAttack>(Lifetime.Scoped);
            builder.Register<PlayerHolder>(Lifetime.Scoped);
            builder.Register<IStart, PlayerSpawner>(Lifetime.Scoped);
            
            // Combo
            builder.RegisterInstance(comboParamManager);
            builder.Register<ComboMultiplier>(Lifetime.Scoped).As<IComboMultiplier>();
            builder.Register<ComboCounter>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<ComboViewMono>();

            // Tower
            builder.Register<TowerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<TowerMono>>();
            builder.Register<IStart, TowerSpawner>(Lifetime.Scoped);
 
            // Enemy
            builder.Register<EnemyPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<EnemyMono>>();
            builder.Register<EnemyBuilder>(Lifetime.Scoped).As<IEnemyBuilder>();

            builder.Register<EnemyAttackDecider>(Lifetime.Scoped);
            builder.Register<EnemySpawner>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<EnemyCluster>(Lifetime.Scoped);

            builder.Register<EnemyHighlightUpdater>(Lifetime.Scoped).AsImplementedInterfaces();

            // Viewer
            builder.RegisterComponentInHierarchy<ViewerViewMono>();

            // IrritatedGuage
            builder.RegisterComponentInHierarchy<IrritatedViewMono>();

            // View
            builder.RegisterComponentInHierarchy<StreamViewMono>();
            builder.RegisterComponentInHierarchy<TimerViewMono>();
            builder.RegisterComponentInHierarchy<StreamerViewMono>();
            
            // Battle
            builder.Register<EndSceneSelector>(Lifetime.Scoped).As<IStart>().AsSelf();

            // Test
            builder.RegisterComponentInHierarchy<PlayerTestInput>();

            // Parameters
            /*comment*/
            builder.Register<CommentParamsServer>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<CommentPosition>();
            builder.RegisterInstance(commentParamManager);

            /*enemy*/
            builder.RegisterInstance(enemyParamManager);
            builder.RegisterInstance(enemyParamManager.enemyLevelDesignParam);
            builder.Register<EnemyTimeLineParamContainer>(Lifetime.Scoped).As<IEnemyTimeLineParamContainer>();
            builder.Register<EnemyParamDataContainer>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterInstance(
                new EnemyLevelDesignParamDataBuilder(builder, enemyParamManager.enemyLevelDesignParam));

            EnemyPositionMono SetUpEnemyPositionMono()
            {
                var lanePositionMono = FindObjectOfType<LanePositionMono>();
                var enemyPositionMono = new GameObject().AddComponent<EnemyPositionMono>();

                foreach (var laneContainer in lanePositionMono.lanePositionContainers)
                {
                    var enemyContainer = new EnemySpawnedPositionContainer();
                    foreach (var lanePosition in laneContainer.lanePositions)
                        enemyContainer.enemySpawnedPoints.Add(lanePosition.enemySpawnedPosition);
                    enemyPositionMono.enemySpawnedPositionContainers.Add(enemyContainer);
                }

                enemyPositionMono.enemyDespawnedPoint = lanePositionMono.enemyDespawnedPoint;
                return enemyPositionMono;
            }

            builder.RegisterComponent(SetUpEnemyPositionMono());
            builder.Register<WaveState>(Lifetime.Scoped); 
            builder.Register<EnemySpawnPoint>(Lifetime.Scoped).As<IEnemySpawnPoint>();


            /*stream*/
            builder.RegisterInstance(new StreamParamDataBuilder(builder, streamParam));

            /*player*/
            PlayerPositionMono SetUpPlayerPositionMono()
            {
                var lanePositionMono = FindObjectOfType<LanePositionMono>();
                var playerPositionMono = new GameObject().AddComponent<PlayerPositionMono>();
                playerPositionMono.playerSpawnedPoint = lanePositionMono.playerSpawnedPosition;
                playerPositionMono.attackEffectDespawnedPoint = lanePositionMono.attackEffectDespawnedPoint;
                return playerPositionMono;
            }

            builder.RegisterInstance(new PlayerPositionMonoBuilder(builder, SetUpPlayerPositionMono()));

            // builder.RegisterInstance(new PlayerParamDataBuilder(builder, playerParamManager));
            builder.RegisterInstance(playerParamManager);
            builder.Register<PlayerParamDataContainer>(Lifetime.Scoped).As<IPlayerParamDataContainer>();
            builder.Register<PlayerHpParamData>(Lifetime.Scoped).As<IPlayerHpParamData>();
            builder.Register<PlayerHp>(Lifetime.Scoped).AsSelf().As<IPlayerHp>();
            builder.Register<PlayerAttackedCounter>(Lifetime.Scoped);
            

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
            
            
            // Debug
            RegisterDebugInput(builder);
        }

        static void RegisterDebugInput(IContainerBuilder builder)
        {
            var waveDebugInput = new GameObject().AddComponent<WaveDebugInputMono>();
            builder.RegisterComponent(waveDebugInput);
        } 
    }
}