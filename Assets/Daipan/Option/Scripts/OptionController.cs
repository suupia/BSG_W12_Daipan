#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Interfaces;
using VContainer;
using System.Linq;

namespace Daipan.Option.Scripts
{
    public class OptionController : IHandleOption , IInputOption
    {
        IOptionContent? _currentOptionContent { get; set; }
        IEnumerable<IOptionContent>? _optionContents;
        public bool IsOpening { private set; get; }

        [Inject]
        public OptionController(IEnumerable<IOptionContent>? optionContents)
        {
            _optionContents = optionContents;
            foreach (var option in _optionContents!) option.SetIHandle(this);
            _currentOptionContent = _optionContents.Where(x => x.OptionContent == OptionContent.Main).FirstOrDefault();
        }


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
            Debug.Log("Open Option!!");
            Prepare();
            IsOpening = true;
            _currentOptionContent?.Prepare();
        }

        public void CloseOption()
        {
            Debug.Log("Close Option!!");
            IsOpening = false;
        }

        public void Prepare()
        {
            _currentOptionContent = _optionContents.Where(x => x.OptionContent == OptionContent.Main).FirstOrDefault();
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
