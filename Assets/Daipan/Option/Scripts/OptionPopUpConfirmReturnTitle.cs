#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Interfaces;

namespace Daipan.Option.Scripts
{
    public class OptionPopUpConfirmReturnTitle : IOptionPopUp
    {
        myContent _myContent;
        IHandleOption _handleOption = null!;
        Func<myContent, IOptionPopUp?> _transitionFunc = null!;

        public void Prepare()
        {
            _myContent = myContent.No;
        }

        public void Select()
        {
            switch (_myContent)
            {
                case myContent.Yes:
                    Debug.Log($"Select : {_myContent}");
                    _handleOption.CloseOption();
                    break;
                case myContent.No:
                    Debug.Log($"Select : {_myContent}");
                    var nextOption = _transitionFunc(_myContent);
                    if (nextOption != null) _handleOption.SetCurrentOption(nextOption);
                    break;
            }
        }
        public void MoveCursor(MoveCursorDirectionEnum moveCursorDirection)
        {
            if (moveCursorDirection == MoveCursorDirectionEnum.Down)
            {
                if (_myContent == myContent.No)
                {
                    _myContent = myContent.Yes;
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

        public void RegisterTransition(Func<myContent, IOptionPopUp?> transitionFunc)
        {
            _transitionFunc = transitionFunc;
        }

        public enum myContent
        {
            Yes,
            No
        }
    }
}