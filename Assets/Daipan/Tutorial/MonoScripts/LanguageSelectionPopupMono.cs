#nullable enable
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

namespace Daipan.Tutorial.MonoScripts
{
    public sealed class LanguageSelectionPopupMono : MonoBehaviour
    {
        [SerializeField] GameObject popupView = null!;

        // リング
        [SerializeField]
        RectTransform satellites;
        [SerializeField]
        RectTransform ling;
        [SerializeField]
        float rotateSpeed;


        void Awake()
        {
            HidePopup();

            // リング
            DOVirtual.Float(0f,0.999f,rotateSpeed, value =>
            {
                ActivateSatellites(value);
                ling.eulerAngles = new Vector3(0, 0, 360f * value);
            }).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
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
    } 
}

