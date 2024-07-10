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
                        if(myContent == OptionPopUpMain.myContent.ReturnTitle)
                            return _optionContents.FirstOrDefault(x => x is OptionPopUpConfirmReturnTitle);

                        return null;
                    });
                }
                if (option is OptionPopUpConfirmReturnTitle optionPopUpConfirmReturnTitle)
                {
                    optionPopUpConfirmReturnTitle.RegisterTransition(myContent =>
                    {
                        if(myContent == OptionPopUpConfirmReturnTitle.myContent.No)
                            return _optionContents.FirstOrDefault(x => x is OptionPopUpMain);

                        return null;
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
            Debug.Log("Option:Open Option!!");
            Time.timeScale = 0;
            Prepare();
            IsOpening = true;
            CurrentOptionContent?.Prepare();
        }

        public void CloseOption()
        {
            Debug.Log("Option:Close Option!!");
            Time.timeScale = 1;
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
