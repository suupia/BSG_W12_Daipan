using Daipan.Battle.scripts;
using Daipan.Battle.Scripts;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Core.Interfaces;
using Daipan.Core.Scripts;
using Daipan.Daipan;
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
using Daipan.Option.Scripts;
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
using Daipan.Tutorial.Interfaces;
using Daipan.Tutorial.MonoScripts;
using Daipan.Tutorial.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Daipan.Tutorial
{
    public sealed class TutorialScope : LifetimeScope
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

        protected override void Configure(IContainerBuilder builder)
        {
            // Stream
            DaipanScope.RegisterStream(builder, streamParam);
    
            // Comment
            DaipanScope.RegisterComment(builder, commentParamManager);
    
            // Player
            DaipanScope.RegisterPlayer(builder, playerParamManager);
            builder.Register<PlayerBuilderTutorial>(Lifetime.Scoped).As<IPlayerBuilder>();
            builder.Register<AttackExecutor>(Lifetime.Transient);
            builder.Register<AttackExecutorTutorial>(Lifetime.Transient).As<IAttackExecutor>(); 
            builder.Register<PlayerAttackEffectBuilderTutorial>(Lifetime.Scoped).As<IPlayerAttackEffectBuilder>();
            builder.Register<PlayerInputTutorial>(Lifetime.Transient).As<IPlayerInput>();
            builder.Register<PlayerOnAttackedTutorial>(Lifetime.Transient).As<IPlayerOnAttacked>();
            
            // Combo
            DaipanScope.RegisterCombo(builder, comboParamManager);
    
            // Tower
            DaipanScope.RegisterTower(builder, towerParams);
    
            // Enemy
            DaipanScope.RegisterEnemy(builder, enemyParamsManager);
            builder.Register<EnemySpawner>(Lifetime.Scoped);
            builder.Register<EnemySpawnerTutorial>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<EnemyBuilderTutorial>(Lifetime.Scoped).As<IEnemyBuilder>();

            // Irritated
            DaipanScope.RegisterIrritated(builder, irritatedParams);
    
            // View
            DaipanScope.RegisterView(builder);
    
            // Battle
            DaipanScope.RegisterBattle(builder);
    
            // InputSerial  
            DaipanScope.RegisterInputSerial(builder);
            
            // Option
            DaipanScope.RegisterOption(builder);
            
            // Tutorial
            builder.Register<TutorialFacilitator>(Lifetime.Scoped).AsSelf().As<IUpdate>();
            RegisterTutorialContents(builder);
            builder.Register<SpeechEventManager>(Lifetime.Scoped);
            
            builder.RegisterComponentInHierarchy<DownloadGaugeViewMono>();
            builder.RegisterComponentInHierarchy<LanguageSelectionPopupMono>();
            builder.RegisterComponentInHierarchy<BlackScreenViewMono>();
            builder.RegisterComponentInHierarchy<SpeechBubbleMono>();
            builder.RegisterComponentInHierarchy<PushEnterTextViewMono>();
            builder.RegisterComponentInHierarchy<AimTopStreamerViewMono>();
            builder.RegisterComponentInHierarchy<StandbyStreamingViewMono>();

            
            // Updater
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<Starter>();
                entryPoints.Add<Updater>();
            });


            // Debug
            RegisterDebugInput(builder);
        }

        static void RegisterTutorialContents(IContainerBuilder builder)
        {
            builder.Register<DisplayBlackScreenWithProgress>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<LanguageSelection>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<FadeInTutorialStart>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<UICatIntroduce>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<RedEnemyTutorial>(Lifetime.Scoped).As<ITutorialContent>().AsSelf();
            builder.Register<SequentialEnemyTutorial>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<ShowWhiteCommentsTutorial>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<ShowAntiCommentsTutorial>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<DaipanCutscene>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<CatSpeaksAfterDaipan>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<AimForTopStreamer>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<StartActualGame>(Lifetime.Scoped).As<ITutorialContent>();
        }

        static void RegisterDebugInput(IContainerBuilder builder)
        {
            // var waveDebugInput = new GameObject().AddComponent<DebugWaveInputMono>();
            // builder.RegisterComponent(waveDebugInput);
        }
    }
}