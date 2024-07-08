#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Scripts;

namespace Daipan.Option.Interfaces
{
    public interface IInputOption
    {
        void Select();
        void MoveCursor(MoveCursorDirection moveCursorDirection);
    }
    public interface IHandleOption
    {
        void SetCurrentOption(IOptionContent optionContent);
        void OpenOption();
        void CloseOption();
    }
}