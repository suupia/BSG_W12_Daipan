#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Interfaces;
using VContainer;

namespace Daipan.Option.Scripts
{
    public class OptionPopUpLanguage : IOptionPopUp
    {
        const OptionContentEnum optionContent = OptionContentEnum.Main;
        public OptionContentEnum OptionContent
        {
            get => optionContent;
        }
        myContent _myContent;
        IHandleOption _handleOption = null!;

        public void Prepare()
        {
            _myContent = myContent.BGM;
        }

        public void Select()
        {
            switch (_myContent)
            {
                case myContent.BGM:
                    Debug.Log($"Select : {_myContent}");
                    break;
                case myContent.SE:
                    Debug.Log($"Select : {_myContent}");
                    break;
                case myContent.IsShaking:
                    Debug.Log($"Select : {_myContent}");
                    break;
                case myContent.ReturnTitle:
                    Debug.Log($"Select : {_myContent}");
                    _handleOption.SetCurrentOption(OptionContentEnum.ConfirmReturnTitle);
                    break;

            }
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
            }
        }
        public void SetIHandle(IHandleOption handleOption)
        {
            _handleOption = handleOption;
        }

        enum myContent
        {
            BGM,
            SE,
            IsShaking,
            ReturnTitle,
            Language,
        }
    }
}