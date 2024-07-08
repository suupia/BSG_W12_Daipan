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
        IOptionPopUpContent? CurrentOptionContent { get; set; }
        readonly List<IOptionPopUpContent> _optionContents;
        public bool IsOpening { private set; get; }

        [Inject]
        public OptionController(IEnumerable<IOptionPopUpContent> optionContents)
        {
            _optionContents = optionContents.ToList();
            foreach (var option in _optionContents) option.SetIHandle(this);
            CurrentOptionContent = _optionContents
                .FirstOrDefault(x => x.OptionContent == OptionContentEnum.Main);
        }


        public void Select()
        {
            CurrentOptionContent?.Select();
        }

        public void MoveCursor(MoveCursorDirectionEnum moveCursorDirection)
        {
            CurrentOptionContent?.MoveCursor(moveCursorDirection);
        }

        public void SetCurrentOption(OptionContentEnum optionContent)
        {
            CurrentOptionContent = _optionContents
                .FirstOrDefault(x => x.OptionContent == optionContent);
        }

        public void OpenOption()
        {
            Debug.Log("Open Option!!");
            Prepare();
            IsOpening = true;
            CurrentOptionContent?.Prepare();
        }

        public void CloseOption()
        {
            Debug.Log("Close Option!!");
            IsOpening = false;
        }

        public void Prepare()
        {
            CurrentOptionContent = _optionContents.FirstOrDefault(x => x.OptionContent == OptionContentEnum.Main);
        }

    }

    public enum MoveCursorDirectionEnum
    {
        Up,
        Right,
        Down,
        Left
    }

    public enum OptionContentEnum
    {
        Main,
        ConfirmReturnTitle
    }
}
