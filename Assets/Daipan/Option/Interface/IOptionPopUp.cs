#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Scripts;

namespace Daipan.Option.Interfaces
{
    public interface IOptionPopUp
    {
        void Prepare();
        void Select();
        void MoveCursor(MoveCursorDirectionEnum moveCursorDirection);
        void SetIHandle(IHandleOption handleOption);
    }
}