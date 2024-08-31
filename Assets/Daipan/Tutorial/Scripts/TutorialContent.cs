#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.InputSerial.Scripts;
using Daipan.Option.Scripts;
using Daipan.Sound.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Streamer.MonoScripts;
using Daipan.Tutorial.Interfaces;
using Daipan.Tutorial.MonoScripts;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;
using Cysharp.Threading.Tasks;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;

namespace Daipan.Tutorial.Scripts
{
    public abstract class AbstractTutorialContent : ITutorialContent, IDisposable
    {
        public abstract void Execute();
        public abstract bool IsCompleted();
        protected readonly CompositeDisposable Disposables = new();

        public void Dispose()
        {
            foreach (var disposable in Disposables) disposable.Dispose();
        }

        ~AbstractTutorialContent()
        {
            Dispose();
        }
    }

    public class LanguageSelection : AbstractTutorialContent
    {
        readonly LanguageConfig _languageConfig;
        readonly InputSerialManager _inputSerialManager;
        readonly LanguageSelectionPopupMono _languageSelectionPopupMono;
        bool Completed { get; set; }

        public LanguageSelection(
            LanguageConfig languageConfig
            , InputSerialManager inputSerialManager
            , LanguageSelectionPopupMono languageSelectionPopupMono
        )
        {
            _languageConfig = languageConfig;
            _inputSerialManager = inputSerialManager;
            _languageSelectionPopupMono = languageSelectionPopupMono;
        }

        public override void Execute()
        {
            Debug.Log("Language selection...");
            _languageSelectionPopupMono.ShowPopup();
            Disposables.Add(Observable.EveryUpdate()
                .Where(_ => !Completed)
                .Subscribe(_ =>
                {
                    if (_inputSerialManager.GetButtonBlue())
                    {
                        _languageConfig.CurrentLanguage = LanguageEnum.Japanese;
                        Debug.Log("Language set to Japanese");
                        _languageSelectionPopupMono.HidePopup();
                        Completed = true;
                    }
                    else if (_inputSerialManager.GetButtonYellow())
                    {
                        _languageConfig.CurrentLanguage = LanguageEnum.English;
                        Debug.Log("Language set to English");
                        _languageSelectionPopupMono.HidePopup();
                        Completed = true;
                    }
                }));
        }

        public override bool IsCompleted()
        {
            return Completed;
        }
    }

    public sealed class DisplayBlackScreenWithProgress : AbstractTutorialContent
    {
        readonly DownloadGaugeViewMono _gaugeViewMono;
        const float FillAmountPerSec = 0.2f;
        bool Completed { get; set; }

        public DisplayBlackScreenWithProgress(
            DownloadGaugeViewMono gaugeViewMono
        )
        {
            _gaugeViewMono = gaugeViewMono;
        }

        public override void Execute()
        {
            _gaugeViewMono.Show();
            SoundManager.Instance?.FadOutBgm(1.0f);

            Disposables.Add(Observable.EveryUpdate()
                .Where(_ => !Completed)
                .Subscribe(_ =>
                {
                    Debug.Log("Displaying black screen with download progress...");
                    // Debug.Log($"_gaugeViewMono.CurrentFillAmount: {_gaugeViewMono.CurrentFillAmount}");
                    _gaugeViewMono.SetGaugeValue(_gaugeViewMono.CurrentFillAmount + FillAmountPerSec * Time.deltaTime);
                    if (_gaugeViewMono.CurrentFillAmount >= 0.5f) Completed = true;
                }));
        }

        public override bool IsCompleted()
        {
            return Completed;
        }
    }


    public class FadeInTutorialStart : AbstractTutorialContent
    {
        readonly DownloadGaugeViewMono _gaugeViewMono;
        readonly BlackScreenViewMono _blackScreenViewMono;
        const float FillAmountPerSec = 0.2f;
        bool Completed { get; set; }

        public FadeInTutorialStart(
            DownloadGaugeViewMono gaugeViewMono
            , BlackScreenViewMono blackScreenViewMono
        )
        {
            _gaugeViewMono = gaugeViewMono;
            _blackScreenViewMono = blackScreenViewMono;
        }

        public override void Execute()
        {
            _gaugeViewMono.Show();
            Disposables.Add(
                Observable.EveryUpdate()
                    .Where(_ => !Completed)
                    .Subscribe(_ =>
                    {
                        Debug.Log("Displaying black screen with download progress...");
                        _gaugeViewMono.SetGaugeValue(_gaugeViewMono.CurrentFillAmount +
                                                     FillAmountPerSec * Time.deltaTime);
                        if (_gaugeViewMono.CurrentFillAmount >= 1.0f)
                            _blackScreenViewMono.FadeOut(1, () =>
                            {
                                _gaugeViewMono.Hide();
                                Completed = true;
                            });
                    }));
        }


        public override bool IsCompleted()
        {
            return Completed;
        }
    }

    public class UICatIntroduce : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly LanguageConfig _languageConfig;

        public UICatIntroduce(
            SpeechEventManager speechEventManager
            , LanguageConfig languageConfig
        )
        {
            _speechEventManager = speechEventManager;
            _languageConfig = languageConfig;
        }

        public override void Execute()
        {
            Debug.Log("Streamer wakes up...");
            Debug.Log("Cat speaks...");
            UnityEngine.Object.FindObjectOfType<SpeechBubbleMono>().IsStartTutorial = true;
            _speechEventManager.SetSpeechEvent(SpeechEventBuilder.BuildUICatIntroduce(_languageConfig.CurrentLanguage));

            SoundManager.Instance?.PlayBgm(BgmEnum.Tutorial);
        }

        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd();
        }
    }

    public class BlueEnemyTutorial : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly EnemySpawnerTutorial _enemySpawnerTutorial;
        readonly LanguageConfig _languageConfig;
        public bool IsSuccess { get; private set; }

        public BlueEnemyTutorial(
            SpeechEventManager speechEventManager
            , EnemySpawnerTutorial enemySpawnerTutorial
            , LanguageConfig languageConfig
        )
        {
            _speechEventManager = speechEventManager;
            _enemySpawnerTutorial = enemySpawnerTutorial;
            _languageConfig = languageConfig;
        }


        public override void Execute()
        {
            Debug.Log("Tutorial: Defeat the blue enemy...");
            _speechEventManager.SetSpeechEvent(
                SpeechEventBuilder.BuildBlueEnemyTutorial(this, _languageConfig.CurrentLanguage));

            _enemySpawnerTutorial.SpawnEnemyByType(EnemyEnum.Blue);
        }

        public override bool IsCompleted()
        {
            return IsSuccess && _speechEventManager.IsEnd();
        }

        public void SetSuccess()
        {
            Debug.Log($"RedEnemyTutorial SetIsSuccess");
            IsSuccess = true;
            _speechEventManager.MoveNext();
        }
    }

    public class SequentialEnemyTutorial : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly EnemySpawnerTutorial _enemySpawnerTutorial;
        readonly LanguageConfig _languageConfig;
        public bool IsSuccess { get; private set; }

        public SequentialEnemyTutorial(
            SpeechEventManager speechEventManager
            , EnemySpawnerTutorial enemySpawnerTutorial
            , LanguageConfig languageConfig
        )
        {
            _speechEventManager = speechEventManager;
            _enemySpawnerTutorial = enemySpawnerTutorial;
            _languageConfig = languageConfig;
        }

        public override void Execute()
        {
            Debug.Log("Tutorial: Defeat enemies in sequence...");
            _speechEventManager.SetSpeechEvent(
                SpeechEventBuilder.BuildSequentialEnemyTutorial(this, _languageConfig.CurrentLanguage));

            var intervalSec = 1f; // スポーンの間隔
            var enemyEnums = new Queue<EnemyEnum>(new[] { EnemyEnum.Blue, EnemyEnum.Yellow, EnemyEnum.Red });

            Disposables.Add(
                Observable.Interval(TimeSpan.FromSeconds(intervalSec))
                    .Take(enemyEnums.Count)
                    .Subscribe(
                        _ => { _enemySpawnerTutorial.SpawnEnemyByType(enemyEnums.Dequeue()); },
                        _ => { Debug.Log("Enemy spawn completed"); }
                    ));
        }

        public override bool IsCompleted()
        {
            return IsSuccess && _speechEventManager.IsEnd();
        }

        public void SetSuccess()
        {
            IsSuccess = true;
            _speechEventManager.MoveNext();
        }
    }

    public class TotemEnemyTutorial : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly EnemySpawnerTutorial _enemySpawnerTutorial;
        readonly LanguageConfig _languageConfig;
        public bool IsSuccess { get; private set; }

        public TotemEnemyTutorial(
            SpeechEventManager speechEventManager
            , EnemySpawnerTutorial enemySpawnerTutorial
            , LanguageConfig languageConfig
        )
        {
            _speechEventManager = speechEventManager;
            _enemySpawnerTutorial = enemySpawnerTutorial;
            _languageConfig = languageConfig;
        }

        public override void Execute()
        {
            Debug.Log("Tutorial: Defeat totem enemy...");
            _speechEventManager.SetSpeechEvent(
                SpeechEventBuilder.BuildTotemEnemyTutorial(this, _languageConfig.CurrentLanguage));
            _enemySpawnerTutorial.SpawnEnemyByType(EnemyEnum.Totem2);
        }

        public override bool IsCompleted()
        {
            return IsSuccess && _speechEventManager.IsEnd();
        }

        public void SetSuccess()
        {
            IsSuccess = true;
            _speechEventManager.MoveNext();
        }
    }

    public class ShowWhiteCommentsTutorial : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly CommentSpawner _commentSpawner;
        readonly LanguageConfig _languageConfig;
        bool CanMoveNext { get; set; }

        public ShowWhiteCommentsTutorial(
            SpeechEventManager speechEventManager
            , CommentSpawner commentSpawner
            , LanguageConfig languageConfig
        )
        {
            _speechEventManager = speechEventManager;
            _commentSpawner = commentSpawner;
            _languageConfig = languageConfig;
        }

        public override void Execute()
        {
            Debug.Log("Displaying white comments...");
            _speechEventManager.SetSpeechEvent(
                SpeechEventBuilder.BuildShowWitheCommentsTutorial(this, _languageConfig.CurrentLanguage));

            var intervalSec = 0.5f; // スポーンの間隔
            var commentCount = 3;
            var delaySec = 2.0f; // すべてのコメントが表示された後の待機時間 

            Disposables.Add(
                Observable.Interval(TimeSpan.FromSeconds(intervalSec))
                    .Take(commentCount)
                    .Subscribe(
                        _ => { _commentSpawner.SpawnCommentByType(CommentEnum.Normal); },
                        _ =>
                        {
                            Debug.Log("Comment spawn completed");
                            Observable.Timer(TimeSpan.FromSeconds(delaySec))
                                .Subscribe(_ =>
                                {
                                    CanMoveNext = true;
                                    Debug.Log("ShowWhiteCommentsTutorial Can move next");
                                })
                                .AddTo(Disposables);
                        }
                    )
            );
        }

        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd() && CanMoveNext;
        }
    }

    public class ForcedMissTutorial : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly CommentSpawner _commentSpawner;
        readonly EnemySpawnerTutorial _enemySpawnerTutorial;
        readonly LanguageConfig _languageConfig;
        readonly IrritatedValue _irritatedValue;
        readonly AttackExecutor _attackExecutor;
        public bool IsMissed { get; set; }
        bool CanMoveNext { get; set; }

        public ForcedMissTutorial(
            SpeechEventManager speechEventManager
            , CommentSpawner commentSpawner
            , EnemySpawnerTutorial enemySpawnerTutorial
            , LanguageConfig languageConfig
            , IrritatedValue irritatedValue
            , AttackExecutor attackExecutor  // IAttackExecutorはAttackExecutorTutorialになっていて、Decoratorを妥協している
        )
        {
            _speechEventManager = speechEventManager;
            _commentSpawner = commentSpawner;
            _enemySpawnerTutorial = enemySpawnerTutorial;
            _languageConfig = languageConfig;
            _irritatedValue = irritatedValue;
            _attackExecutor = attackExecutor;
        }

        public override void Execute()
        {
            Debug.Log("Tutorial: forced miss...");
            // todo : いいかんじのメッセージを表示
            _speechEventManager.SetSpeechEvent(SpeechEventBuilder.BuildForcedMissTutorial(this, _languageConfig.CurrentLanguage));
            ExecuteAsync().Forget();
        }
        
        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd() && CanMoveNext;
        }

        async UniTaskVoid ExecuteAsync()
        {
            // 雑魚敵とボスを生成する
            const float enemyIntervalSec = 0.5f; // スポーンの間隔
            var enemyEnums = new Queue<EnemyEnum>(new[]
            {
                EnemyEnum.Blue, EnemyEnum.RedBoss, EnemyEnum.Red, EnemyEnum.YellowBoss, EnemyEnum.Yellow,
                EnemyEnum.BlueBoss
            });
            Disposables.Add(
                Observable.Interval(TimeSpan.FromSeconds(enemyIntervalSec))
                    .Take(enemyEnums.Count)
                    .Subscribe(
                        _ => { _enemySpawnerTutorial.SpawnEnemyByType(enemyEnums.Dequeue()); },
                        _ => { Debug.Log("Enemy spawn completed"); }
                    )
            );

            const double delaySecForMissed = 2.5f;
            await UniTask.Delay(TimeSpan.FromSeconds(delaySecForMissed));
            Debug.Log("Forced miss...");
            var playerMono = Object.FindObjectOfType<PlayerMono>();
            if (playerMono != null)
                _attackExecutor.FireAttackEffect(playerMono, PlayerColor.Red);
            else
                Debug.LogWarning("PlayerMono is not set");
            IsMissed = true;
            _speechEventManager.MoveNext();
            
            const double delaySecForAntiComment = 2.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(delaySecForAntiComment));
            for (int i = 0; i < 3; i++) 
            {
                _commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
            }

            // スポットライトを当てる
            var irritatedGaugeSpotLight = Object.FindObjectOfType<IrritatedGaugeSpotLightMono>();
            if(irritatedGaugeSpotLight != null) irritatedGaugeSpotLight.Show(); 
            
            await UniTask.WaitUntil(() => _irritatedValue.IsFull); // ここでイライラゲージがmaxになったかどうかを判定
            
            if(irritatedGaugeSpotLight != null) irritatedGaugeSpotLight.Hide();  
            CanMoveNext = true;
            Debug.Log("ForcedMissTutorial Can move next");  
            // todo : イライラゲージmaxになったら次のContentに遷移
            _speechEventManager.MoveNext(); 
        } 
    }

    public class ShowAntiCommentsTutorial : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly CommentSpawner _commentSpawner;
        readonly EnemySpawnerTutorial _enemySpawnerTutorial;
        readonly LanguageConfig _languageConfig;
        bool CanMoveNext { get; set; }

        public ShowAntiCommentsTutorial(
            SpeechEventManager speechEventManager
            , CommentSpawner commentSpawner
            , EnemySpawnerTutorial enemySpawnerTutorial
            , LanguageConfig languageConfig
        )
        {
            _speechEventManager = speechEventManager;
            _commentSpawner = commentSpawner;
            _enemySpawnerTutorial = enemySpawnerTutorial;
            _languageConfig = languageConfig;
        }

        public override void Execute()
        {
            Debug.Log("Showing anti-comments with sound effects...");
            _speechEventManager.SetSpeechEvent(
                SpeechEventBuilder.BuildShowAntiCommentsTutorial(this, _languageConfig.CurrentLanguage));

            // アンチコメントを生成→終わったら遷移可能
            const float commentIntervalSec = 0.5f; // スポーンの間隔
            const int commentCount = 3;
            const float delaySec = 2.0f; // すべてのコメントが表示された後の待機時間 
            Disposables.Add(
                Observable.Interval(TimeSpan.FromSeconds(commentIntervalSec))
                    .Take(commentCount)
                    .Subscribe(
                        _ => { _commentSpawner.SpawnCommentByType(CommentEnum.Spiky); },
                        _ =>
                        {
                            Debug.Log("Comment spawn completed");
                            Observable.Timer(TimeSpan.FromSeconds(delaySec))
                                .Subscribe(_ =>
                                {
                                    CanMoveNext = true;
                                    Debug.Log("ShowAntiCommentsTutorial Can move next");
                                })
                                .AddTo(Disposables);
                        }
                    )
            );

            // 雑魚敵とボスも生成する
            const float enemyIntervalSec = 0.5f; // スポーンの間隔
            var enemyEnums = new Queue<EnemyEnum>(new[]
            {
                EnemyEnum.Blue, EnemyEnum.RedBoss, EnemyEnum.Red, EnemyEnum.YellowBoss, EnemyEnum.Yellow,
                EnemyEnum.BlueBoss
            });
            Disposables.Add(
                Observable.Interval(TimeSpan.FromSeconds(enemyIntervalSec))
                    .Take(enemyEnums.Count)
                    .Subscribe(
                        _ => { _enemySpawnerTutorial.SpawnEnemyByType(enemyEnums.Dequeue()); },
                        _ => { Debug.Log("Enemy spawn completed"); }
                    )
            );
        }

        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd() && CanMoveNext;
        }
    }


    public class DaipanCutscene : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly IrritatedValue _irritatedValue;
        readonly DaipanExecutor _daipanExecutor;
        readonly PushEnterTextViewMono _pushEnterTextViewMono;
        readonly LanguageConfig _languageConfig;
        public bool IsDaipaned => _daipanExecutor.DaipanCount >= 1;

        public DaipanCutscene(
            SpeechEventManager speechEventManager
            , IrritatedValue irritatedValue
            , DaipanExecutor daipanExecutor
            , PushEnterTextViewMono pushEnterTextViewMono
            , LanguageConfig languageConfig
        )
        {
            _speechEventManager = speechEventManager;
            _irritatedValue = irritatedValue;
            _daipanExecutor = daipanExecutor;
            _pushEnterTextViewMono = pushEnterTextViewMono;
            _languageConfig = languageConfig;
        }

        public override void Execute()
        {
            Debug.Log("Displaying special cutscene...");
            Debug.Log("Anger gauge animation...");
            _speechEventManager.SetSpeechEvent(
                SpeechEventBuilder.BuildShowDaipanCutsceneTutorial(this, _languageConfig.CurrentLanguage));

            const float fillRatioPerSec = 0.2f;
            Disposables.Add(
                Observable.EveryUpdate()
                    .Where(_ => _daipanExecutor.DaipanCount < 1)
                    .Subscribe(
                        _ =>
                        {
                            _irritatedValue.IncreaseValue(fillRatioPerSec * _irritatedValue.MaxValue * Time.deltaTime);
                        },
                        _ => { Debug.Log($"IrritatedValue: {_irritatedValue.Value}"); }
                    )
            );

            Disposables.Add(
                Observable.EveryValueChanged(_irritatedValue, irritatedValue => irritatedValue.Value)
                    .Subscribe(
                        _ =>
                        {
                            if (_irritatedValue.IsFull)
                                _pushEnterTextViewMono.Show();
                            else
                                _pushEnterTextViewMono.Hide();
                        })
            );
        }

        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd() && IsDaipaned;
        }
    }


    public class CatSpeaksAfterDaipan : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly LanguageConfig _languageConfig;

        public CatSpeaksAfterDaipan(
            SpeechEventManager speechEventManager
            , LanguageConfig languageConfig
        )
        {
            _speechEventManager = speechEventManager;
            _languageConfig = languageConfig;
        }

        public override void Execute()
        {
            Debug.Log("Cat speaks more...");
            _speechEventManager.SetSpeechEvent(
                SpeechEventBuilder.BuildCatSpeaksAfterDaipan(this, _languageConfig.CurrentLanguage));

            Disposables.Add(
                Observable.EveryUpdate()
                    .Subscribe(
                        _ => { },
                        _ => { Debug.Log("Completed CatSpeaksAfterDaipan"); }
                    )
            );
        }

        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd();
        }
    }

    public class AimForTopStreamer : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly AimTopStreamerViewMono _aimTopStreamerViewMono;
        bool Completed { get; set; }

        public AimForTopStreamer(
            SpeechEventManager speechEventManager
            , AimTopStreamerViewMono aimTopStreamerViewMono
        )
        {
            _speechEventManager = speechEventManager;
            _aimTopStreamerViewMono = aimTopStreamerViewMono;
        }

        public override void Execute()
        {
            Debug.Log("Aim for top streamer...");
            _aimTopStreamerViewMono.Show();

            // 少し待つ
            const float displaySec = 2.0f;
            Observable.Timer(TimeSpan.FromSeconds(displaySec))
                .Subscribe(_ =>
                {
                    Completed = true;
                    _aimTopStreamerViewMono.Hide();
                });
        }

        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd() && Completed;
        }
    }

    public class StartActualGame : AbstractTutorialContent
    {
        readonly BlackScreenViewMono _blackScreenViewMono;
        readonly StandbyStreamingViewMono _standbyStreamingViewMono;
        readonly InputSerialManager _inputSerialManager;
        bool Completed { get; set; }

        public StartActualGame(
            BlackScreenViewMono blackScreenViewMono
            , StandbyStreamingViewMono standbyStreamingViewMono
            , InputSerialManager inputSerialManager
        )
        {
            _blackScreenViewMono = blackScreenViewMono;
            _standbyStreamingViewMono = standbyStreamingViewMono;
            _inputSerialManager = inputSerialManager;
        }

        public override void Execute()
        {
            Debug.Log("Starting actual game...");
            _blackScreenViewMono.FadeIn(1.0f, () =>
            {
                // 配信待機所を表示
                _standbyStreamingViewMono.Show();
                // すこししてからフェードアウトして、次のシーンへ
                const float displaySec = 2.0f;
                Observable.Timer(TimeSpan.FromSeconds(displaySec))
                    .Subscribe(_ =>
                    {
                        _blackScreenViewMono.FadeOut(0.2f, () =>
                        {
                            SceneTransition.TransitioningScene(SceneName.DaipanScene);
                            Completed = true;
                        });
                    });
            });
        }

        public override bool IsCompleted()
        {
            return Completed;
        }
    }
}