#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.InputSerial.Scripts;
using Daipan.Option.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Streamer.Scripts;
using Daipan.Tutorial.Interfaces;
using Daipan.Tutorial.MonoScripts;
using R3;
using UnityEngine;

namespace Daipan.Tutorial.Scripts
{
    public abstract class AbstractTutorialContent : ITutorialContent, IDisposable
    {
        public abstract void Execute();
        public abstract bool IsCompleted();
        protected bool Completed { get; set; }
        protected readonly IList<IDisposable> Disposables = new List<IDisposable>();

        public void Dispose()
        {
            foreach (var disposable in Disposables) disposable.Dispose();
        }

        ~AbstractTutorialContent()
        {
            Dispose();
        }
    }

    public sealed class DisplayBlackScreenWithProgress : AbstractTutorialContent
    {
        readonly DownloadGaugeViewMono _gaugeViewMono;
        const float FillAmountPerSec = 0.2f;

        public DisplayBlackScreenWithProgress(DownloadGaugeViewMono gaugeViewMono)
        {
            _gaugeViewMono = gaugeViewMono;
        }

        public override void Execute()
        {
            Disposables.Add(Observable.EveryUpdate()
                .Where(_ => !Completed)
                .Subscribe(_ =>
                {
                    Debug.Log("Displaying black screen with download progress...");
                    _gaugeViewMono.SetGaugeValue(_gaugeViewMono.CurrentFillAmount + FillAmountPerSec * Time.deltaTime);
                    if (_gaugeViewMono.CurrentFillAmount >= 0.5f) Completed = true;
                }));
        }

        public override bool IsCompleted()
        {
            return Completed;
        }
    }

    public class LanguageSelection : AbstractTutorialContent
    {
        readonly LanguageConfig _languageConfig;
        readonly InputSerialManager _inputSerialManager;
        readonly LanguageSelectionPopupMono _languageSelectionPopupMono;

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
                        _languageConfig.CurrentLanguage = LanguageConfig.LanguageEnum.Japanese;
                        Debug.Log("Language set to Japanese");
                        _languageSelectionPopupMono.HidePopup();
                        Completed = true;
                    }
                    else if (_inputSerialManager.GetButtonYellow())
                    {
                        _languageConfig.CurrentLanguage = LanguageConfig.LanguageEnum.English;
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

    public class FadeInTutorialStart : AbstractTutorialContent
    {
        readonly DownloadGaugeViewMono _gaugeViewMono;
        readonly BlackScreenViewMono _blackScreenViewMono;
        const float FillAmountPerSec = 0.2f;

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
            Disposables.Add(
                Observable.EveryUpdate()
                .Where(_ => !Completed)
                .Subscribe(_ =>
                {
                    Debug.Log("Displaying black screen with download progress...");
                    _gaugeViewMono.SetGaugeValue(_gaugeViewMono.CurrentFillAmount + FillAmountPerSec * Time.deltaTime);
                    if (_gaugeViewMono.CurrentFillAmount >= 1.0f)  _blackScreenViewMono.FadeOut(1, () =>
                    {
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
        public UICatIntroduce(
            SpeechEventManager speechEventManager
            )
        {
            _speechEventManager = speechEventManager;
        }
        public override void Execute()
        {
            Debug.Log("Streamer wakes up...");
            Debug.Log("Cat speaks...");
            _speechEventManager.SetSpeechEvent(SpeechEventBuilder.BuildUICatIntroduce()); 

        }

        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd();
        }
    }

    public class RedEnemyTutorial : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly EnemySpawnerTutorial _enemySpawnerTutorial;
        public RedEnemyTutorial(
            SpeechEventManager speechEventManager
            ,EnemySpawnerTutorial enemySpawnerTutorial
        )
        {
            _speechEventManager = speechEventManager;
            _enemySpawnerTutorial = enemySpawnerTutorial;
        }
        public bool IsSuccess { get; private set; }  // UICatのSpeechEventの分岐で使用
        public override void Execute()
        {
            Debug.Log("Tutorial: Defeat the red enemy...");
            _speechEventManager.SetSpeechEvent(SpeechEventBuilder.BuildRedEnemyTutorial(this));

            _enemySpawnerTutorial.SpawnEnemyByType(EnemyEnum.Red);

        }

        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd();
        }

        public void SetIsSuccess(bool isSuccess)
        {
            Debug.Log($"RedEnemyTutorial isSuccess: {isSuccess}");
            IsSuccess = isSuccess;
            // これで強制的に次のスピーチに進む（危険かも）
            while (!_speechEventManager.IsEnd())
            {
                _speechEventManager.MoveNext();
                Debug.Log($"RedEnemyTutorial MoveNext _speechEventManager.CurrentEvent.Message: {_speechEventManager.CurrentEvent.Message}");
            }

        }
    }

    public class SequentialEnemyTutorial : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly EnemySpawnerTutorial _enemySpawnerTutorial;
        
        public SequentialEnemyTutorial(
        SpeechEventManager speechEventManager
        , EnemySpawnerTutorial enemySpawnerTutorial
        )
        {
            _speechEventManager = speechEventManager;
            _enemySpawnerTutorial = enemySpawnerTutorial;
        }

        public override void Execute()
        {
            Debug.Log("Tutorial: Defeat enemies in sequence...");
           _speechEventManager.SetSpeechEvent(SpeechEventBuilder.BuildSequentialEnemyTutorial(this));
           
           float intervalSec = 1f; // スポーンの間隔
           var enemyEnums = new Queue<EnemyEnum>(new []{EnemyEnum.Blue, EnemyEnum.Yellow, EnemyEnum.Red});

           Disposables.Add(
               Observable.Interval(TimeSpan.FromSeconds(intervalSec))
               .Take(enemyEnums.Count)
               .Subscribe(
                   _ => { _enemySpawnerTutorial.SpawnEnemyByType(enemyEnums.Dequeue()); },
                   _ =>
                   {
                       Debug.Log("Enemy spawn completed");

                   }
               ));
           
        }

        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd();
        }

        public void MoveNextSpeech()
        {
            // これで強制的に次のスピーチに進む（危険かも）
            while (!_speechEventManager.IsEnd())
            {
                _speechEventManager.MoveNext();
                Debug.Log($"RedEnemyTutorial MoveNext _speechEventManager.CurrentEvent.Message: {_speechEventManager.CurrentEvent.Message}");
            }
        }
    }

    public class ShowWhiteCommentsTutorial : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly CommentSpawner _commentSpawner;  
        bool CanMoveNext { get; set; }
        public ShowWhiteCommentsTutorial(
            SpeechEventManager speechEventManager
            , CommentSpawner commentSpawner
        )
        {
            _speechEventManager = speechEventManager;
            _commentSpawner = commentSpawner;
        }

        public override void Execute()
        {
            Debug.Log("Displaying white comments...");
            _speechEventManager.SetSpeechEvent(SpeechEventBuilder.BuildShowWitheCommentsTutorial(this));
           
            float intervalSec = 0.5f; // スポーンの間隔
            int commentCount = 3;
            float delaySec = 2.0f; // すべてのコメントが表示された後の待機時間 

            Disposables.Add(
                Observable.Interval(System.TimeSpan.FromSeconds(intervalSec))
                .Take(commentCount)
                .Subscribe(
                    _ => { _commentSpawner.SpawnCommentByType(CommentEnum.Normal); },
                    _ =>
                    {
                        Debug.Log( "Comment spawn completed");
                        Observable.Timer(TimeSpan.FromSeconds(delaySec))
                            .Subscribe(_ =>
                            {
                                CanMoveNext = true;
                                Debug.Log("ShowWhiteCommentsTutorial Can move next");
                            })
                            .AddTo(Disposables);;
                    }
                )
            );
            
        }

        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd() && CanMoveNext;
        }
    }

    public class ShowAntiCommentsTutorial : AbstractTutorialContent
    {
        readonly SpeechEventManager _speechEventManager;
        readonly CommentSpawner _commentSpawner;
        bool CanMoveNext { get; set; }
        public ShowAntiCommentsTutorial(
            SpeechEventManager speechEventManager
            , CommentSpawner commentSpawner
        )
        {
            _speechEventManager = speechEventManager;
            _commentSpawner = commentSpawner;
        }
        public override void Execute()
        {
            Debug.Log("Showing anti-comments with sound effects...");
            _speechEventManager.SetSpeechEvent(SpeechEventBuilder.BuildShowAntiCommentsTutorial(this));
            
            float intervalSec = 0.5f; // スポーンの間隔
            int commentCount = 3;
            float delaySec = 2.0f; // すべてのコメントが表示された後の待機時間 

            Disposables.Add(
                Observable.Interval(System.TimeSpan.FromSeconds(intervalSec))
                .Take(commentCount)
                .Subscribe(
                    _ => { _commentSpawner.SpawnCommentByType(CommentEnum.Spiky); },
                    _ =>
                    {
                        Debug.Log( "Comment spawn completed");
                        Observable.Timer(TimeSpan.FromSeconds(delaySec))
                            .Subscribe(_ =>
                            {
                                CanMoveNext = true;
                                Debug.Log("ShowAntiCommentsTutorial Can move next");
                            })
                            .AddTo(Disposables);;
                    }
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
        bool CanMoveNext { get; set; }
        public DaipanCutscene(
            SpeechEventManager speechEventManager
            , IrritatedValue irritatedValue 
        )
        {
            _speechEventManager = speechEventManager;
            _irritatedValue = irritatedValue;
        }

        public override void Execute()
        {
            Debug.Log("Displaying special cutscene...");
            Debug.Log("Anger gauge animation...");
            _speechEventManager.SetSpeechEvent(SpeechEventBuilder.BuildShowDaipanCutsceneTutorial(this));
            
            const float fillRatioPerSec = 0.2f;
            Disposables.Add(
                Observable.EveryUpdate()
                    .Subscribe(
                        _ =>
                        {
                           _irritatedValue.IncreaseValue(fillRatioPerSec * _irritatedValue.MaxValue * Time.deltaTime); 
                        },
                        _ =>
                        {
                            Debug.Log($"IrritatedValue: {_irritatedValue.Value}"); 
                        }
                    )
            );
        }

        public override bool IsCompleted()
        {
            return _speechEventManager.IsEnd();
        }
    }


    public class CatSpeaksAfterDaipan : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Cat speaks more...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    public class AimForTopStreamer : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Aim for top streamer...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    public class StartActualGame : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Starting actual game...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }
}