#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.Option.Interfaces
{
    public interface IOptionContent
    {
        IEnumerable _transitionableOptions { get; set; }
        void Select();
        void MoveCursor();
    }
}