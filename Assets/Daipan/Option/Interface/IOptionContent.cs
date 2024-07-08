#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Scripts;

namespace Daipan.Option.Interfaces
{
    public interface IOptionContent
    {
        OptionContent OptionContent { get; }
        void Prepare();
        void Select();
        void MoveCursor(MoveCursorDirection moveCursorDirection);
        void SetIHandle(IHandleOption handleOption);
    }
}