#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Scripts;
using VContainer;
using R3;

public class OptionLanguageViewMono : MonoBehaviour
{
    [SerializeField] GameObject cursorObject = null!;

    [Inject]
    public void Initialize(OptionPopUpMain optionPopUpMain)
    {
        Observable.EveryValueChanged(optionPopUpMain, x => x.CurrentContent).
            Subscribe(_ =>
            {
                if (optionPopUpMain.CurrentContent == OptionPopUpMain.CurrentContents.Language)
                {
                    cursorObject.SetActive(true);
                }
                else
                {
                    cursorObject.SetActive(false);
                }
            }).
            AddTo(this);
    }
}
