#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Scripts;

namespace Daipan.Option.Interfaces
{
    public interface IOptionContent
    {
        IEnumerable _transitionableOptions { get; set; }
        void Select();
        void MoveCursor(MoveCursorDirection moveCursorDirection);
    }
}