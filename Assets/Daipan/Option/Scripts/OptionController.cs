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
        IOptionPopUp? CurrentOptionContent { get; set; }
        readonly List<IOptionPopUp> _optionContents;
        public bool IsOpening { private set; get; }

        [Inject]
        public OptionController(IEnumerable<IOptionPopUp> optionContents)
        {
            _optionContents = optionContents.ToList();
            foreach (var option in _optionContents) option.SetIHandle(this);
            CurrentOptionContent = _optionContents
                .FirstOrDefault(x => x is OptionPopUpMain);

            foreach (var option in _optionContents)
            {
                if (option is OptionPopUpMain optionPopUpMain)
                {
                    optionPopUpMain.RegisterTransition(myContent =>
                    {
                        return _optionContents.FirstOrDefault(x => x is OptionPopUpConfirmReturnTitle);
                    });
                }
                if (option is OptionPopUpConfirmReturnTitle optionPopUpConfirmReturnTitle)
                {
                    optionPopUpConfirmReturnTitle.RegisterTransition(myContent =>
                    {
                        return _optionContents.FirstOrDefault(x => x is OptionPopUpMain);
                    });
                }
            }
        }


        public void Select()
        {
            CurrentOptionContent?.Select();
        }

        public void MoveCursor(MoveCursorDirectionEnum moveCursorDirection)
        {
            CurrentOptionContent?.MoveCursor(moveCursorDirection);
        }

        public void SetCurrentOption(IOptionPopUp optionPopUp)
        {
            CurrentOptionContent = _optionContents
                .FirstOrDefault(x => x.GetType() == optionPopUp.GetType());
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
            CurrentOptionContent = _optionContents.FirstOrDefault(x => x is OptionPopUpMain);
        }

    }

    public enum MoveCursorDirectionEnum
    {
        Up,
        Right,
        Down,
        Left
    }
    
}
