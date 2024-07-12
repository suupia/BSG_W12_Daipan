using Daipan.Battle.scripts;
using Daipan.Battle.Scripts;
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
using Daipan.LevelDesign.EndScene;
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
using Daipan.Streamer.MonoScripts;
using Daipan.Tower.MonoScripts;
using Daipan.Tower.Scripts;
using Daipan.Option.Interfaces;
using Daipan.Option.Scripts;
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

        [FormerlySerializedAs("enemyParamManager")] [SerializeField]
        EnemyParamsManager enemyParamsManager = null!;

        [FormerlySerializedAs("commentParamsManager")] [SerializeField]
        CommentParamManager commentParamManager = null!;

        [SerializeField] IrritatedParams irritatedParams = null!;
        [SerializeField] TowerParams towerParams = null!;
        [SerializeField] ComboParamManager comboParamManager = null!;

        [SerializeField] EndSceneTransitionParam endSceneTransitionParam = null!;

        public static void RegisterStream(IContainerBuilder builder, StreamParam streamParam)
        {
            // Parameters 
            builder.RegisterInstance(new StreamParamDataBuilder(builder, streamParam));
            builder.RegisterInstance(streamParam.viewer);
            builder.RegisterInstance(streamParam.daipan);
            // Stream
            builder.Register<StreamPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<StreamMono>>();
            builder.Register<ViewerNumber>(Lifetime.Scoped);
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

        public static void RegisterEnemy(IContainerBuilder builder, EnemyParamsManager enemyParamsManager)
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
            builder.RegisterInstance(enemyParamsManager);
            builder.RegisterInstance(enemyParamsManager.enemyLevelDesignParam);
            builder.Register<EnemySpawnPoint>(Lifetime.Scoped).As<IEnemySpawnPoint>();
            builder.Register<EnemyWaveParamContainer>(Lifetime.Scoped).As<IEnemyWaveParamContainer>();
            builder.Register<EnemyParamDataContainer>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterInstance(new EnemyLevelDesignParamData(enemyParamsManager.enemyLevelDesignParam));
            builder.Register<EnemyOnAttackedBuilder>(Lifetime.Transient);
            // Enemy
            builder.Register<EnemyPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<EnemyMono>>();
            builder.Register<EnemyCluster>(Lifetime.Scoped);
            builder.Register<EnemyAttackDecider>(Lifetime.Scoped);
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
            builder.Register<EndSceneSelector>(Lifetime.Scoped);
        }

        public static void RegisterInputSerial(IContainerBuilder builder)
        {
            // InputSerial
            builder.Register<SerialInput>(Lifetime.Scoped);
            builder.Register<InputSerialManager>(Lifetime.Scoped);
        }

        public static void RegisterOption(IContainerBuilder builder)
        {
            builder.Register<OptionController>(Lifetime.Scoped).As<IHandleOption>().As<IInputOption>();

            // PopUp
            builder.Register<OptionPopUpMain>(Lifetime.Scoped).As<IOptionPopUp>();
            builder.Register<OptionPopUpConfirmReturnTitle>(Lifetime.Scoped).As<IOptionPopUp>();

            // Config
            builder.Register<LanguageConfig>(Lifetime.Scoped);
            builder.Register<DaipanShakingConfig>(Lifetime.Scoped);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            // Stream
            RegisterStream(builder, streamParam);

            // Comment
            RegisterComment(builder, commentParamManager);

            // Player
            RegisterPlayer(builder, playerParamManager);
            builder.Register<PlayerBuilder>(Lifetime.Scoped).As<IPlayerBuilder>();
            builder.Register<AttackExecutor>(Lifetime.Transient).As<IAttackExecutor>();
            builder.Register<PlayerAttackEffectBuilder>(Lifetime.Scoped).As<IPlayerAttackEffectBuilder>();
            builder.Register<PlayerInput>(Lifetime.Transient).As<IPlayerInput>();
            builder.Register<PlayerOnAttacked>(Lifetime.Transient).As<IPlayerOnAttacked>();
            builder.Register<PlayerMissedAttackCounter>(Lifetime.Transient);

            // Combo
            RegisterCombo(builder, comboParamManager);

            // Tower
            RegisterTower(builder, towerParams);

            // Enemy
            RegisterEnemy(builder, enemyParamsManager);
            builder.Register<EnemyWaveSpawnerCounter>(Lifetime.Scoped).As<IUpdate>();
            builder.Register<EnemySpawner>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<EnemyEnumSelector>(Lifetime.Scoped).As<IEnemyEnumSelector>();
            builder.Register<EnemyBuilder>(Lifetime.Scoped).As<IEnemyBuilder>();
            builder.Register<EnemySpecialOnAttacked>(Lifetime.Scoped);
            builder.Register<FinalBossActionDecider>(Lifetime.Scoped);
            builder.Register<FinalBossPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<FinalBossMono>>();
            builder.Register<FinalBossBuilder>(Lifetime.Scoped);
            builder.Register<FinalBossSpawner>(Lifetime.Scoped);

            // Irritated
            RegisterIrritated(builder, irritatedParams);

            // View
            RegisterView(builder);

            // Battle
            RegisterBattle(builder);
            builder.RegisterComponentInHierarchy<WaveTextMono>();

            // InputSerial  
            RegisterInputSerial(builder);

            // Option
            RegisterOption(builder);

            // Result
            builder.Register<ResultState>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<ResultViewMono>();
            
            // EndScene
            builder.RegisterInstance(endSceneTransitionParam);

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
            var debugWaveInputMono = new GameObject().AddComponent<DebugWaveInputMono>();
            builder.RegisterComponent(debugWaveInputMono);
            var debugEndSceneInputMono = new GameObject().AddComponent<DebugEndSceneInputMono>();
            builder.RegisterComponent(debugEndSceneInputMono);
        }
    }
}