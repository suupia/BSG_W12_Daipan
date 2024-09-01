#nullable enable
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using VContainer;
using Daipan.Option.Scripts;
using DG.Tweening;

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

        [SerializeField]
        Image selectedEnglish = null!;

        // リング
        [SerializeField]
        RectTransform satellites = null!;
        [SerializeField]
        RectTransform ling = null!;
        [SerializeField]
        float rotateSpeed;

        // EnterKey
        [SerializeField]
        RectTransform triangle = null!;
        [SerializeField]
        RectTransform enterKey = null!;
        [SerializeField]
        float moveTriangleSpeed;
        [SerializeField]
        float selectEnterSpeed;

        private LanguageConfig _languageConfig = null!;

        [Inject]
        public void Initialize(LanguageConfig languageConfig)
        {
            _languageConfig = languageConfig;
        }

        void Awake()
        {
            HidePopup(false);
        }


        private void Update()
        {
            float value = Time.time * rotateSpeed - MathF.Floor(Time.time * rotateSpeed);
            ActivateSatellites(value);
            ling.eulerAngles = new Vector3(0, 0, -360f * value);

            value = Time.time * moveTriangleSpeed - MathF.Floor(Time.time * moveTriangleSpeed);
            //value = Mathf.Abs(value - 0.5f) * 2;
            value = MathF.Sin(MathF.PI * value);
            triangle.anchoredPosition = new Vector3(0f, 10f * value, 0f);

            UpdateLanguage();
        }
        public void ShowPopup()
        {
            popupView.SetActive(true);
        }
        
        public void HidePopup(bool isAnimation = true)
        {
            if (isAnimation)
                enterKey.DOScale(1.5f, selectEnterSpeed).SetLoops(2, LoopType.Yoyo)
                    .OnComplete(() => popupView.SetActive(false));
            else
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
                selectedEnglish.gameObject.SetActive(true);
            }
            else if(_languageConfig.CurrentLanguage == LanguageEnum.Japanese)
            {
                onJapanese.gameObject.SetActive(true);
                offJapanese.gameObject.SetActive(false);
                onEnglish.gameObject.SetActive(false);
                offEnglish.gameObject.SetActive(true);
                selectedEnglish.gameObject.SetActive(false);
            }
        }
    } 
}

