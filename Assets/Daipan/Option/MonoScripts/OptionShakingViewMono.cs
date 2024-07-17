#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Scripts;
using VContainer;
using R3;
using System.Drawing;

public class OptionShakingViewMono : MonoBehaviour
{
    [SerializeField] GameObject cursorObject = null!;
    [SerializeField] GameObject onActive = null!;
    [SerializeField] GameObject onInactive = null!;
    [SerializeField] GameObject offActive = null!;
    [SerializeField] GameObject offInactive = null!;



    [Inject]
    public void Initialize(OptionPopUpMain optionPopUpMain ,DaipanShakingConfig daipanShakingConfig)
    {
        Observable.EveryValueChanged(optionPopUpMain, x => x.CurrentContent).
            Subscribe(_ =>
            {
                if (optionPopUpMain.CurrentContent == OptionPopUpMain.CurrentContents.IsShaking)
                {
                    cursorObject.SetActive(true);
                }
                else
                {
                    cursorObject.SetActive(false);
                }
            }).
            AddTo(this);

        Observable.EveryValueChanged(daipanShakingConfig, x => x.IsShaking).
            Subscribe(_ =>
            {
                onActive.SetActive(daipanShakingConfig.IsShaking);
                onInactive.SetActive(!daipanShakingConfig.IsShaking);
                offActive.SetActive(!daipanShakingConfig.IsShaking);
                offInactive.SetActive(daipanShakingConfig.IsShaking);
            }).
            AddTo(this);
    }
}
