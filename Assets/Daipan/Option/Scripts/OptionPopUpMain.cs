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
        myContent _myContent;
        IHandleOption _handleOption = null!;
        Func<myContent, IOptionPopUp?> _transitionFunc = null!; 

        public void Prepare()
        {
            _myContent = myContent.BGM;
        }

        public void Select()
        {
            var nextOption = _transitionFunc(myContent.ReturnTitle);
            if(nextOption != null) _handleOption.SetCurrentOption(nextOption);
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

        public void RegisterTransition(Func<myContent, IOptionPopUp?> transitionFunc)
        {
            _transitionFunc = transitionFunc;
        }
        public enum myContent
        {
            BGM,
            SE,
            IsShaking,
            ReturnTitle,
            Language,
        }
    }
}