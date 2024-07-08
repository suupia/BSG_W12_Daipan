#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Daipan.Tutorial.MonoScripts
{
    public class DownloadGaugeViewMono : MonoBehaviour
    {
        [SerializeField] GameObject viewObject = null!;
        [SerializeField] Image gaugeImage = null!;

        public float CurrentFillAmount => gaugeImage.fillAmount;

        void Awake()
        {
           Hide(); 
        }

        public void SetGaugeValue(float value)
        {
            gaugeImage.fillAmount = value;
        }
        public void Show()
        {
            viewObject.SetActive(true);
        }
        public void Hide()
        {
            gaugeImage.fillAmount = 0;
            viewObject.SetActive(false);
        }
    }
}