#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Interfaces;
using Daipan.Sound.MonoScripts;
using VContainer;
using Daipan.Battle.scripts;

namespace Daipan.Option.Scripts
{
    public class OptionPopUpMain : IOptionPopUp
    {
       readonly  LanguageConfig _languageConfig;
       readonly DaipanShakingConfig _daipanShakingConfig;


        public CurrentContents CurrentContent { get; private set; }
        IHandleOption _handleOption = null!;
        Func<CurrentContents, IOptionPopUp?> _transitionFunc = null!;

        [Inject]
        public OptionPopUpMain(LanguageConfig languageConfig
            ,DaipanShakingConfig daipanShakingConfig)
        {
            _languageConfig = languageConfig;
            _daipanShakingConfig = daipanShakingConfig;
        }

        public void Prepare()
        {
            CurrentContent = CurrentContents.Resume;
        }

        public void Select()
        {
            if (CurrentContent == CurrentContents.Resume) _handleOption.CloseOption();
            else if(CurrentContent == CurrentContents.ReturnTitle)
            {
                _handleOption.CloseOption();
                SceneTransition.TransitioningScene(SceneName.TitleScene);
            }
            //var nextOption = _transitionFunc(CurrentContent);
            //if (nextOption != null) _handleOption.SetCurrentOption(nextOption);
        }
        public void MoveCursor(MoveCursorDirectionEnum moveCursorDirection)
        {
            if(moveCursorDirection == MoveCursorDirectionEnum.Down)
            {
                if(CurrentContent == CurrentContents.ReturnTitle)
                {
                    CurrentContent = CurrentContents.Resume;
                }
                else
                {
                    CurrentContent++;
                }
                Debug.Log($"Option:Move : {CurrentContent}");

                return;
            }
            if(moveCursorDirection == MoveCursorDirectionEnum.Right)
            {
                switch (CurrentContent)
                {
                    case CurrentContents.BGM:
                        SoundManager.BgmVolume = SoundManager.BgmVolume + 1;
                        break;
                    case CurrentContents.SE:
                        SoundManager.SeVolume = SoundManager.SeVolume + 1;
                        break;
                    case CurrentContents.IsShaking:
                        _daipanShakingConfig.IsShaking = false;
                        Debug.Log("Option:Shaking OFF");
                        break;
                    case CurrentContents.Language:
                        _languageConfig.CurrentLanguage = LanguageEnum.English;
                        Debug.Log("Option:Language = English");
                        break;
                }
                return;
            }
            if(moveCursorDirection == MoveCursorDirectionEnum.Left)
            {
                switch (CurrentContent)
                {
                    case CurrentContents.BGM:
                        SoundManager.BgmVolume = SoundManager.BgmVolume - 1;
                        break;
                    case CurrentContents.SE:
                        SoundManager.SeVolume = SoundManager.SeVolume - 1;
                        break;
                    case CurrentContents.IsShaking:
                        _daipanShakingConfig.IsShaking = true;
                        Debug.Log("Option:Shaking ON");
                        break;
                    case CurrentContents.Language:
                        _languageConfig.CurrentLanguage = LanguageEnum.Japanese;
                        Debug.Log("Option:Language = Japanese");
                        break;
                }
                return;
            }
        }
        public void SetIHandle(IHandleOption handleOption)
        {
            _handleOption = handleOption;
        }

        public void RegisterTransition(Func<CurrentContents, IOptionPopUp?> transitionFunc)
        {
            _transitionFunc = transitionFunc;
        }
        public enum CurrentContents
        {
            Resume,
            BGM,
            SE,
            IsShaking,
            Language,
            ReturnTitle,
        }
    }
}