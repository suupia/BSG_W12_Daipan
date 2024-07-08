#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Interfaces;
using VContainer;

namespace Daipan.Option.Scripts
{
    public class OptionMain : IOptionContent
    {
        const OptionContent optionContent = OptionContent.Main;
        public OptionContent OptionContent
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
                    Debug.Log("BGMだよ");
                    break;
                case myContent.SE:
                    Debug.Log("SEだよ");
                    break;
                case myContent.IsShaking:
                    Debug.Log("IsShakingだよ");
                    break;
                case myContent.ReturnTitle:
                    Debug.Log("ReturnTitleだよ");
                    break;

            }
        }
        public void MoveCursor(MoveCursorDirection moveCursorDirection)
        {
            if(moveCursorDirection == MoveCursorDirection.Down)
            {
                if(_myContent == myContent.ReturnTitle)
                {
                    _myContent = myContent.BGM;
                }
                else
                {
                    _myContent++;
                }
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
            ReturnTitle
        }
    }
}