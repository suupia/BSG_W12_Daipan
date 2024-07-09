#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Interfaces;

namespace Daipan.Option.Scripts
{
    public class OptionConfirmReturnTitle : IOptionContent
    {
        const OptionContentEnum optionContent = OptionContentEnum.ConfirmReturnTitle;
        public OptionContentEnum OptionContent
        {
            get => optionContent;
        }
        myContent _myContent;
        IHandleOption _handleOption = null!;

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
                    _handleOption.SetCurrentOption(OptionContentEnum.Main);
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

        enum myContent
        {
            Yes,
            No
        }
    }
}