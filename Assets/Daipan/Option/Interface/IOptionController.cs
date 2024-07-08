#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Scripts;

namespace Daipan.Option.Interfaces
{
    public interface IInputOption
    {
        bool IsOpening { get; }
        void Select();
        void MoveCursor(MoveCursorDirection moveCursorDirection);
        void OpenOption();
        void CloseOption();
    }
    public interface IHandleOption
    {
        void SetCurrentOption(IOptionContent optionContent);
        void CloseOption();
    }
}