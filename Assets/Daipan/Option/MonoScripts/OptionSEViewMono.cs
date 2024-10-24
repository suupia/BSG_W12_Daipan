#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Scripts;
using VContainer;
using R3;
using UnityEngine.UI;
using Daipan.Sound.MonoScripts;

public class OptionSEViewMono : MonoBehaviour
{
    [SerializeField] GameObject cursorObject = null!;
    [SerializeField] Image volume = null!;
    [SerializeField] GameObject triangleCursor = null!;
    [SerializeField] RectTransform triangleLeft = null!;
    [SerializeField] RectTransform triangleRight = null!;

    [Inject]
    public void Initialize(OptionPopUpMain optionPopUpMain)
    {
        Observable.EveryValueChanged(optionPopUpMain, x => x.CurrentContent).
            Subscribe(_ =>
            {
                if (optionPopUpMain.CurrentContent == OptionPopUpMain.CurrentContents.SE)
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

    private void Update()
    {
        volume.fillAmount = SoundManager.SeVolume / 7;
        float cursorPositionRatio = (SoundManager.SeVolume - 1) / 6f;
        if (cursorPositionRatio < 0)
        {
            triangleCursor.SetActive(false);
        }
        else
        {
            triangleCursor.SetActive(true);
        }

        triangleCursor.GetComponent<RectTransform>().position
            = triangleLeft.position * (1 - cursorPositionRatio)
            + triangleRight.position * cursorPositionRatio;

    }
}
