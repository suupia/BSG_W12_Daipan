#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.Option.Interfaces
{
    public interface IOptionController
    {
        IOptionContent _currentOptionContent { get; set; }
        void Select();
        void MoveCursor();
        void SetCurrentOption(IOptionContent optionContent);
    }
}