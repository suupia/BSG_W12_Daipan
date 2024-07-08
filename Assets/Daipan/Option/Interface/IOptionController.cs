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
        void MoveCursor(MoveCursorDirectionEnum moveCursorDirection);
        void OpenOption();
        void CloseOption();
    }
    public interface IHandleOption
    {
        void SetCurrentOption(OptionContentEnum optionContent);
        void CloseOption();
    }
}