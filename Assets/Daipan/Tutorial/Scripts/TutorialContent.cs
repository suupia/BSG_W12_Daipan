#nullable enable
using System;
using System.Collections.Generic;
using Daipan.InputSerial.Scripts;
using Daipan.Option.Scripts;
using Daipan.Streamer.Scripts;
using Daipan.Tutorial.Interfaces;
using Daipan.Tutorial.MonoScripts;
using R3;
using UnityEngine;

namespace Daipan.Tutorial.Scripts
{
    internal abstract class AbstractTutorialContent : ITutorialContent, IDisposable
    {
        public abstract void Execute();
        public abstract bool IsCompleted();
        protected bool Completed;
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

    internal sealed class DisplayBlackScreenWithProgress : AbstractTutorialContent
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

    internal class LanguageSelection : AbstractTutorialContent
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

    internal class FadeInTutorialStart : AbstractTutorialContent
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
            Disposables.Add(Observable.EveryUpdate()
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

    internal class UICatSpeaks : AbstractTutorialContent 
    {
        readonly SpeechBubbleMono _speechBubbleMono;
        readonly UICatMessage _uiCatMessage;
        readonly InputSerialManager _inputSerialManager;
        public UICatSpeaks(
            SpeechBubbleMono speechBubbleMono
            ,UICatMessage uiCatMessage
            ,InputSerialManager inputSerialManager
            )
        {
            _speechBubbleMono = speechBubbleMono;
            _uiCatMessage = uiCatMessage;
            _inputSerialManager = inputSerialManager;
        }
        public override void Execute()
        {
            Debug.Log("Streamer wakes up...");
            Debug.Log("Cat speaks...");
            Disposables.Add(Observable.EveryUpdate()
                .Where(_ => !Completed)
                .Subscribe(_ =>
                {
                    if(_inputSerialManager.GetButtonAny()) _speechBubbleMono.ShowSpeechBubble(_uiCatMessage.GetNextMessage());
                }));
        }

        public override bool IsCompleted()
        {
            return Completed;
        }
    }

    internal class RedEnemyTutorial : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Tutorial: Defeat the red enemy...");
            // 敵を倒せれば上手！！
            // そうでなかったら、もう一回！
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class SequentialEnemyTutorial : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Tutorial: Defeat enemies in sequence...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class ShowWhiteComments : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Displaying white comments...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class ShowAntiComments : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Showing anti-comments with sound effects...");
            Debug.Log("Anger gauge animation...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }


    internal class DaipanCutscene : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Displaying special cutscene...");
            // 特別なカットに切り替える
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }


    internal class CatSpeaksAfterDaipan : ITutorialContent
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

    internal class AimForTopStreamer : ITutorialContent
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

    internal class StartActualGame : ITutorialContent
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