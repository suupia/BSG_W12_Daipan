#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Interfaces;

namespace Daipan.Option.Scripts
{
    public class OptionController : IHandleOption , IInputOption
    {
        IOptionContent? _currentOptionContent { get; set; }

        public void Select()
        {
            _currentOptionContent?.Select();
        }
        public void MoveCursor(MoveCursorDirection moveCursorDirection)
        {
            _currentOptionContent?.MoveCursor(moveCursorDirection);
        }

        public void SetCurrentOption(IOptionContent optionContent)
        {
            _currentOptionContent = optionContent;
        }
        public void OpenOption()
        {

        }
        public void CloseOption()
        {

        }
    }

    public enum MoveCursorDirection
    {
        UP,
        Right,
        Down,
        Left
    }

    public enum OptionContent
    {
        Main,
        ConfirmReturnTitle
    }
}
