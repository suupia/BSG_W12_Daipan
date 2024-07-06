using Daipan.Battle.scripts;
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
using Daipan.Streamer.Scripts;
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

        [FormerlySerializedAs("enemyParamsManager")] [SerializeField]
        EnemyParamManager enemyParamManager = null!;

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
            builder.Register<AttackExecutor>(Lifetime.Transient);
            builder.Register<AttackExecutorTutorial>(Lifetime.Transient).As<IAttackExecutor>(); 
            builder.Register<PlayerAttackEffectTutorialBuilder>(Lifetime.Scoped).As<IPlayerAttackEffectBuilder>();
            
            // Combo
            DaipanScope.RegisterCombo(builder, comboParamManager);
    
            // Tower
            DaipanScope.RegisterTower(builder, towerParams);
    
            // Enemy
            DaipanScope.RegisterEnemy(builder, enemyParamManager);
            builder.Register<EnemySpawnerTutorial>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            // Irritated
            DaipanScope.RegisterIrritated(builder, irritatedParams);
    
            // View
            DaipanScope.RegisterView(builder);
    
            // Battle
            DaipanScope.RegisterBattle(builder);
    
            // InputSerial  
            DaipanScope.RegisterInputSerial(builder);

            // Tutorial
            builder.Register<TutorialFacilitator>(Lifetime.Scoped).As<IUpdate>();
            RegisterTutorialContents(builder);

            builder.RegisterComponentInHierarchy<DownloadGaugeViewMono>();
            builder.Register<LanguageConfig>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<LanguageSelectionPopupMono>();
            builder.RegisterComponentInHierarchy<BlackScreenViewMono>();
            builder.RegisterComponentInHierarchy<SpeechBubbleMono>();
            builder.Register<SpeechEventManager>(Lifetime.Transient);
            builder.Register<SpeechEventBuilder>(Lifetime.Transient);

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
            // builder.Register<DisplayBlackScreenWithProgress>(Lifetime.Scoped).As<ITutorialContent>();
            // builder.Register<LanguageSelection>(Lifetime.Scoped).As<ITutorialContent>();
            // builder.Register<FadeInTutorialStart>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<UICatSpeaks>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<RedEnemyTutorial>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<SequentialEnemyTutorial>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<ShowWhiteComments>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<ShowAntiComments>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<DaipanCutscene>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<CatSpeaksAfterDaipan>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<AimForTopStreamer>(Lifetime.Scoped).As<ITutorialContent>();
            builder.Register<StartActualGame>(Lifetime.Scoped).As<ITutorialContent>();
        }

        static void RegisterDebugInput(IContainerBuilder builder)
        {
            var waveDebugInput = new GameObject().AddComponent<WaveDebugInputMono>();
            builder.RegisterComponent(waveDebugInput);
        }
    }
}