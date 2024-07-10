#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Interfaces;
using VContainer;

namespace Daipan.Option.Scripts
{
    public class OptionPopUpMain : IOptionPopUp
    {
       readonly  LanguageConfig _languageConfig;
       readonly DaipanShakingConfig _daipanShakingConfig;


        myContent _myContent;
        IHandleOption _handleOption = null!;
        Func<myContent, IOptionPopUp?> _transitionFunc = null!;

        [Inject]
        public OptionPopUpMain(LanguageConfig languageConfig
            ,DaipanShakingConfig daipanShakingConfig)
        {
            _languageConfig = languageConfig;
            _daipanShakingConfig = daipanShakingConfig;
        }

        public void Prepare()
        {
            _myContent = myContent.BGM;
        }

        public void Select()
        {
            var nextOption = _transitionFunc(_myContent);
            if (nextOption != null) _handleOption.SetCurrentOption(nextOption);
        }
        public void MoveCursor(MoveCursorDirectionEnum moveCursorDirection)
        {
            if(moveCursorDirection == MoveCursorDirectionEnum.Down)
            {
                if(_myContent == myContent.ReturnTitle)
                {
                    _myContent = myContent.BGM;
                }
                else
                {
                    _myContent++;
                }
                Debug.Log($"Move : {_myContent}");

                return;
            }
            if(moveCursorDirection == MoveCursorDirectionEnum.Right)
            {
                switch (_myContent)
                {
                    case myContent.BGM:
                        break;
                    case myContent.SE:
                        break;
                    case myContent.IsShaking:
                        _daipanShakingConfig.IsShaking = false;
                        break;
                    case myContent.Language:
                        _languageConfig.CurrentLanguage = LanguageConfig.LanguageEnum.English;
                        break;
                }
                return;
            }
            if(moveCursorDirection == MoveCursorDirectionEnum.Left)
            {
                switch (_myContent)
                {
                    case myContent.BGM:
                        break;
                    case myContent.SE:
                        break;
                    case myContent.IsShaking:
                        _daipanShakingConfig.IsShaking = true;
                        break;
                    case myContent.Language:
                        _languageConfig.CurrentLanguage = LanguageConfig.LanguageEnum.Japanese;
                        break;
                }
                return;
            }
        }
        public void SetIHandle(IHandleOption handleOption)
        {
            _handleOption = handleOption;
        }

        public void RegisterTransition(Func<myContent, IOptionPopUp?> transitionFunc)
        {
            _transitionFunc = transitionFunc;
        }
        public enum myContent
        {
            BGM,
            SE,
            IsShaking,
            Language,
            ReturnTitle,
        }
    }
}