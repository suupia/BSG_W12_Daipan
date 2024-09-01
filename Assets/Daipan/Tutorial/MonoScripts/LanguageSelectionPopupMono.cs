#nullable enable
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using VContainer;
using Daipan.Option.Scripts;

namespace Daipan.Tutorial.MonoScripts
{
    public sealed class LanguageSelectionPopupMono : MonoBehaviour
    {
        [SerializeField] GameObject popupView = null!;

        // 言語
        [SerializeField]
        Image onJapanese = null!;
        [SerializeField]
        Image offJapanese = null!;
        [SerializeField]
        Image onEnglish = null!;
        [SerializeField]
        Image offEnglish = null!;

        // リング
        [SerializeField]
        RectTransform satellites;
        [SerializeField]
        RectTransform ling;
        [SerializeField]
        float rotateSpeed;

        private LanguageConfig _languageConfig = null!;

        [Inject]
        public void Initialize(LanguageConfig languageConfig)
        {
            _languageConfig = languageConfig;
        }

        void Awake()
        {
            HidePopup();
        }


        private void Update()
        {
            float value = Time.time * rotateSpeed - MathF.Floor(Time.time * rotateSpeed);
            ActivateSatellites(value);
            ling.eulerAngles = new Vector3(0, 0, -360f * value);

            UpdateLanguage();
        }
        public void ShowPopup()
        {
            popupView.SetActive(true);
        }
        
        public void HidePopup()
        {
            popupView.SetActive(false);
        }

        void ActivateSatellites(float index)
        {
            int count = (int)(satellites.childCount * index);
            for (int i = 0; i < satellites.childCount; i++)
            {
                if (i == count)
                    satellites.GetChild(i).gameObject.SetActive(true);
                else
                    satellites.GetChild(i).gameObject.SetActive(false);
            }
        }

        void UpdateLanguage()
        {
            if(_languageConfig.CurrentLanguage == LanguageEnum.English)
            {
                onJapanese.gameObject.SetActive(false);
                offJapanese.gameObject.SetActive(true);
                onEnglish.gameObject.SetActive(true);
                offEnglish.gameObject.SetActive(false);
            }
            else if(_languageConfig.CurrentLanguage == LanguageEnum.Japanese)
            {
                onJapanese.gameObject.SetActive(true);
                offJapanese.gameObject.SetActive(false);
                onEnglish.gameObject.SetActive(false);
                offEnglish.gameObject.SetActive(true);
            }
        }
    } 
}

