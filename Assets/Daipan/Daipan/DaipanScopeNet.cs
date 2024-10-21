#nullable enable
using Cysharp.Threading.Tasks;
using Daipan.Battle.scripts;
using Daipan.Battle.Scripts;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Core;
using Daipan.Core.Interfaces;
using Daipan.Core.Scripts;
using Daipan.DebugInput.MonoScripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.InputSerial.Interfaces;
using Daipan.InputSerial.Scripts;
using Daipan.LevelDesign.Battle.Scripts;
using Daipan.LevelDesign.Combo.Scripts;
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
using Daipan.Option.MonoScripts;
using Daipan.Sound.MonoScripts;
using Daipan.Sound.Scripts;
using Fusion;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Daipan.Daipan

{
    public sealed class DaipanScopeNet : LifetimeScope
    {
        static DaipanScopeNet? _instance;

        public static DaipanScopeNet BuildedContainer
        {
            get
            {
                if (_instance != null) return _instance;
                var daipanScopeNet = FindObjectOfType<DaipanScopeNet>();
                daipanScopeNet.Build();
                return daipanScopeNet;
            }
        }

        [FormerlySerializedAs("streamParameter")] [SerializeField]
        StreamParam streamParam = null!;

        [SerializeField] PlayerParamManager playerParamManager = null!;

        [FormerlySerializedAs("enemyParamManager")] [SerializeField]
        EnemyParamsManager enemyParamsManager = null!;

        [FormerlySerializedAs("commentParamsManager")] [SerializeField]
        CommentParamManager commentParamManager = null!;

        [SerializeField] FinalBossParamManager finalBossParamManager = null!;

        [SerializeField] IrritatedParams irritatedParams = null!;
        [SerializeField] TowerParams towerParams = null!;
        [SerializeField] ComboParamManager comboParamManager = null!;

        [SerializeField] EndSceneTransitionParam endSceneTransitionParam = null!;

        public NetworkRunner? Runner { private get; set; }

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
            builder.Register<WaveProgress>(Lifetime.Scoped);
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
            builder.Register<PlayerPrefabLoaderNetwork>(Lifetime.Scoped).As<IPrefabLoader<PlayerNet>>();
            builder.Register<PlayerHolder>(Lifetime.Scoped);
            builder.Register<ThresholdResetCounter>(Lifetime.Scoped);
            builder.Register<DaipanExecutor>(Lifetime.Scoped);
            builder.Register<IStart, PlayerSpawnerNetwork>(Lifetime.Scoped);
            // Attack
            builder.Register<PlayerAttackEffectPrefabLoaderNetwork>(Lifetime.Scoped).As<IPrefabLoader<PlayerAttackEffectNet>>();
            builder.Register<PlayerAttackEffectSpawnerNetwork>(Lifetime.Scoped).AsImplementedInterfaces();
        }

        public static void RegisterCombo(IContainerBuilder builder, ComboParamManager comboParamManager)
        {
            // Parameters
            builder.RegisterInstance(comboParamManager);
            // Combo
            builder.Register<ComboPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<ComboInstantViewMono>>();
            builder.Register<ComboMultiplier>(Lifetime.Scoped).As<IComboMultiplier>();
            builder.Register<ComboCounter>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<ComboViewMono>();
            builder.Register<ComboSpawner>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<ComboPositionMono>();
            builder.Register<ComboParamsManager>(Lifetime.Scoped);
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
            // Enemy
            builder.Register<EnemyPrefabLoaderNetwork>(Lifetime.Scoped).As<IPrefabLoader<EnemyNet>>();
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
            builder.Register<IrritatedGaugeValue>(Lifetime.Scoped).WithParameter("maxValue", 100);
        }

        public static void RegisterView(IContainerBuilder builder)
        {
            // Viewer
            builder.RegisterComponentInHierarchy<ViewerViewMono>();

            // View
            builder.RegisterComponentInHierarchy<StreamerViewMono>().AsSelf().As<IUpdate>();

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
            builder.RegisterComponentInHierarchy<StreamInputButtonManagerMono>().As<IInputSerialManager>();

            // Enter
            builder.Register<GetEnterKey>(Lifetime.Scoped).As<IGetEnterKey>();
        }

        public static void RegisterOption(IContainerBuilder builder)
        {
            builder.Register<OptionController>(Lifetime.Scoped).As<IHandleOption>().As<IInputOption>().AsSelf();

            // PopUp
            builder.Register<OptionPopUpMain>(Lifetime.Scoped).As<IOptionPopUp>().AsSelf();
            builder.Register<OptionPopUpConfirmReturnTitle>(Lifetime.Scoped).As<IOptionPopUp>();

            // Config
            builder.Register<DaipanShakingConfig>(Lifetime.Scoped);

            // View
            builder.RegisterComponentInHierarchy<OptionPopUpViewMono>();
            builder.RegisterComponentInHierarchy<OptionBGMViewMono>();
            builder.RegisterComponentInHierarchy<OptionSEViewMono>();
            builder.RegisterComponentInHierarchy<OptionShakingViewMono>();
            builder.RegisterComponentInHierarchy<OptionLanguageViewMono>();
            builder.RegisterComponentInHierarchy<OptionResumeViewMono>();
            builder.RegisterComponentInHierarchy<OptionReturnTitleViewMono>();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log($"DaipanScopeNet Configure() builder: {builder}");

            if (Runner == null) Debug.LogWarning("NetworkRunner is null");
            var runner = FindObjectOfType<NetworkRunner>();
            builder.RegisterComponent(runner);

            builder.Register<DTONetWrapper>(Lifetime.Scoped);

            // Stream
            RegisterStream(builder, streamParam);

            // Comment
            RegisterComment(builder, commentParamManager);

            // Player
            RegisterPlayer(builder, playerParamManager);
            builder.Register<PlayerBuilder>(Lifetime.Scoped).As<IPlayerBuilder>();
            builder.Register<AttackExecutor>(Lifetime.Transient).As<IAttackExecutor>();
            builder.Register<PlayerAttackEffectBuilder>(Lifetime.Scoped).As<IPlayerAttackEffectBuilder>();
            builder.Register<StreamerInput>(Lifetime.Transient).As<IPlayerInput>();
            builder.Register<PlayerOnAttacked>(Lifetime.Transient).As<IPlayerOnAttacked>();

            // Combo
            RegisterCombo(builder, comboParamManager);

            // Tower
            RegisterTower(builder, towerParams);

            // Enemy
            RegisterEnemy(builder, enemyParamsManager);
            builder.Register<EnemyOnAttackedBuilder>(Lifetime.Transient);
            builder.RegisterComponentInHierarchy<EnemyWaveSpawnerCounterNet>().AsSelf().AsImplementedInterfaces();
            builder.Register<EnemySpawnerNetwork>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<EnemyEnumSelector>(Lifetime.Scoped).As<IEnemyEnumSelector>();
            builder.Register<EnemyBuilder>(Lifetime.Scoped).As<IEnemyBuilder>();
            builder.Register<EnemySpecialOnAttacked>(Lifetime.Scoped);

            // FinalBoss
            builder.RegisterInstance(finalBossParamManager);
            builder.Register<FinalBossColorChanger>(Lifetime.Scoped);
            builder.Register<FinalBossParamData>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<FinalBossActionDecider>(Lifetime.Scoped);
            builder.Register<FinalBossNetPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<FinalBossNet>>();
            builder.Register<FinalBossOnAttacked>(Lifetime.Scoped);
            builder.Register<FinalBossBuilder>(Lifetime.Scoped);
            builder.Register<FinalBossSpawnerNetwork>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<FinalBossDefeatTracker>(Lifetime.Scoped);

            // Irritated
            RegisterIrritated(builder, irritatedParams);

            // View
            RegisterView(builder);
            builder.RegisterComponentInHierarchy<WaveProgressViewMono>().AsSelf().As<IUpdate>();


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

            // Sound
            builder.Register<DaipanSoundStarter>(Lifetime.Scoped).As<IStart>();

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