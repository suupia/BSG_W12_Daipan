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
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
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

        [FormerlySerializedAs("commentParamsManager")] [SerializeField]
        CommentParamManager commentParamManager = null!;

        [SerializeField] IrritatedParams irritatedParams = null!;
        [SerializeField] TowerParams towerParams = null!;
        [SerializeField] ComboParamManager comboParamManager = null!;

        public static void RegisterStream(IContainerBuilder builder, StreamParam streamParam)
        {
            // Parameters 
            builder.RegisterInstance(new StreamParamDataBuilder(builder, streamParam));
            builder.RegisterInstance(streamParam.viewer);
            builder.RegisterInstance(streamParam.daipan);
            // Stream
            builder.Register<StreamPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<StreamMono>>();
            builder.Register<ViewerNumber>(Lifetime.Scoped);
            builder.Register<StreamStatus>(Lifetime.Scoped);
            builder.Register<IStart, StreamSpawner>(Lifetime.Scoped).AsSelf();
            builder.Register<StreamTimer>(Lifetime.Scoped).AsSelf().As<IStart>().As<IUpdate>();
        }

        public static void RegisterComment(IContainerBuilder builder, CommentParamManager commentParamManager)
        {
            // Parameters 
            builder.RegisterInstance(commentParamManager);
            builder.Register<CommentParamsServer>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<CommentPosition>();
            // Comment 
            builder.Register<CommentPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<CommentMono>>();
            builder.Register<AntiCommentPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<AntiCommentMono>>();
            builder.Register<CommentCluster>(Lifetime.Scoped);
            builder.Register<IUpdate, CommentSpawner>(Lifetime.Scoped).AsSelf();
            builder.Register<AntiCommentCluster>(Lifetime.Scoped);
            builder.Register<IUpdate, AntiCommentRelocate>(Lifetime.Scoped);
        }

        public static void RegisterPlayer(IContainerBuilder builder, PlayerParamManager playerParamManager)
        {
            // Parameters
            PlayerPositionMono SetUpPlayerPositionMono()
            {
                var lanePositionMono = FindObjectOfType<LanePositionMono>();
                var playerPositionMono =
                    new GameObject($"PlayerPositionMono(Runtime)").AddComponent<PlayerPositionMono>();
                playerPositionMono.playerSpawnedPoint = lanePositionMono.playerSpawnedPosition;
                playerPositionMono.playerAttackEffectPosition = new PlayerAttackEffectPosition();
                playerPositionMono.playerAttackEffectPosition.attackEffectSpawnedPoint =
                    lanePositionMono.attackEffectSpawnedPoint;
                playerPositionMono.playerAttackEffectPosition.attackEffectDespawnedPoint =
                    lanePositionMono.attackEffectDespawnedPoint;
                return playerPositionMono;
            }

            builder.RegisterInstance(new PlayerPositionMonoBuilder(builder, SetUpPlayerPositionMono()));
            builder.RegisterInstance(playerParamManager);
            builder.Register<PlayerParamDataContainer>(Lifetime.Scoped).As<IPlayerParamDataContainer>();
            builder.Register<PlayerHpParamData>(Lifetime.Scoped).As<IPlayerHpParamData>();
            builder.Register<PlayerAntiCommentParamData>(Lifetime.Scoped).As<IPlayerAntiCommentParamData>();
            // Player
            builder.Register<PlayerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<PlayerMono>>();
            builder.Register<PlayerHolder>(Lifetime.Scoped);
            builder.Register<PlayerAttackedCounter>(Lifetime.Scoped);
            builder.Register<DaipanExecutor>(Lifetime.Scoped);
            builder.Register<IStart, PlayerSpawner>(Lifetime.Scoped);
            // Attack
            builder.Register<PlayerAttackEffectPrefabLoader>(Lifetime.Scoped)
                .As<IPrefabLoader<PlayerAttackEffectMono>>();
            builder.Register<PlayerAttackEffectSpawner>(Lifetime.Scoped);
            builder.Register<PlayerAttackEffectBuilder>(Lifetime.Scoped);
        }

        public static void RegisterCombo(IContainerBuilder builder, ComboParamManager comboParamManager)
        {
            // Parameters
            builder.RegisterInstance(comboParamManager);
            // Combo
            builder.Register<ComboMultiplier>(Lifetime.Scoped).As<IComboMultiplier>();
            builder.Register<ComboCounter>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<ComboViewMono>();
        }

        public static void RegisterTower(IContainerBuilder builder, TowerParams towerParams)
        {
            // Parameters
            builder.Register<TowerParamsConfig>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<TowerPositionMono>();
            builder.RegisterInstance(towerParams);
            // Tower
            builder.Register<TowerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<TowerMono>>();
            builder.Register<IStart, TowerSpawner>(Lifetime.Scoped);
        }

        public static void RegisterEnemy(IContainerBuilder builder, EnemyParamManager enemyParamManager)
        {
            // Parameters
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
            builder.RegisterInstance(enemyParamManager);
            builder.RegisterInstance(enemyParamManager.enemyLevelDesignParam);
            builder.Register<EnemySpawnPoint>(Lifetime.Scoped).As<IEnemySpawnPoint>();
            builder.Register<EnemyTimeLineParamContainer>(Lifetime.Scoped).As<IEnemyTimeLineParamContainer>();
            builder.Register<EnemyParamDataContainer>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterInstance(
                new EnemyLevelDesignParamDataBuilder(builder, enemyParamManager.enemyLevelDesignParam));
            // Enemy
            builder.Register<EnemyPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<EnemyMono>>();
            builder.Register<EnemyBuilder>(Lifetime.Scoped).As<IEnemyBuilder>();
            builder.Register<EnemyCluster>(Lifetime.Scoped);
            builder.Register<EnemyAttackDecider>(Lifetime.Scoped);
            builder.Register<EnemyTotemOnAttack>(Lifetime.Scoped);
            builder.Register<EnemyHighlightUpdater>(Lifetime.Scoped).AsImplementedInterfaces();
        }
        public static void RegisterIrritated(IContainerBuilder builder, IrritatedParams irritatedParams)
        {
            // Parameters
            builder.RegisterInstance(irritatedParams);
            // IrritatedGauge
            builder.RegisterComponentInHierarchy<IrritatedViewMono>();
            builder.RegisterComponentInHierarchy<IrritatedGaugeBackgroundViewMono>();
            builder.Register<IrritatedValue>(Lifetime.Scoped).WithParameter("maxValue", 100);
        }
        public static void RegisterView(IContainerBuilder builder)
        {
            // Viewer
            builder.RegisterComponentInHierarchy<ViewerViewMono>();

            // View
            builder.RegisterComponentInHierarchy<StreamViewMono>();
            builder.RegisterComponentInHierarchy<WaveProgressViewMono>();
            builder.RegisterComponentInHierarchy<StreamerViewMono>();

            // ShakeDisplay
            builder.RegisterComponentInHierarchy<ShakeDisplayMono>();

        }

        public static void RegisterBattle(IContainerBuilder builder)
        {
            // Battle
            builder.Register<WaveState>(Lifetime.Scoped);
            builder.Register<EndSceneSelector>(Lifetime.Scoped).As<IStart>().AsSelf();
        }

        public static void RegisterInputSerial(IContainerBuilder builder)
        {
            // InputSerial
            builder.Register<SerialInput>(Lifetime.Scoped);
            builder.Register<InputSerialManager>(Lifetime.Scoped);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            // Stream
            RegisterStream(builder, streamParam);
            
            // Comment
            RegisterComment(builder, commentParamManager);
            
            // Player
            RegisterPlayer(builder, playerParamManager);
            builder.Register<AttackExecutor>(Lifetime.Transient).As<IAttackExecutor>();
            
            // Combo
            RegisterCombo(builder, comboParamManager);
            
            // Tower
            RegisterTower(builder, towerParams);
            
            // Enemy
            RegisterEnemy(builder, enemyParamManager);
            builder.Register<EnemySpawner>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();

            // Irritated
            RegisterIrritated(builder, irritatedParams);
            
            // View
            RegisterView(builder);
            
            // Battle
            RegisterBattle(builder);
            
            // InputSerial  
            RegisterInputSerial(builder);
            
            // Updater
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<Starter>();
                entryPoints.Add<Updater>();
            });
            
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