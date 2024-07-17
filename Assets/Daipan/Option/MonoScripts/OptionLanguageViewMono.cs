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
    [SerializeField] GameObject japaneseActive = null!;
    [SerializeField] GameObject japaneseInactive = null!;
    [SerializeField] GameObject englishActive = null!;
    [SerializeField] GameObject englishInactive = null!;

    [Inject]
    public void Initialize(OptionPopUpMain optionPopUpMain , LanguageConfig languageConfig)
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

        Observable.EveryValueChanged(languageConfig, x => x.CurrentLanguage).
            Subscribe(_ =>
            {
                bool isJapanese = languageConfig.CurrentLanguage == LanguageEnum.Japanese;
                japaneseActive.SetActive(isJapanese);
                japaneseInactive.SetActive(!isJapanese);
                englishActive.SetActive(!isJapanese);
                englishInactive.SetActive(isJapanese);
            }).
            AddTo(this);
    }
}
