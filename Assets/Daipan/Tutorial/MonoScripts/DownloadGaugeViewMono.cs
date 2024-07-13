#nullable enable
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Daipan.Tutorial.MonoScripts
{
    public class DownloadGaugeViewMono : MonoBehaviour
    {
        [SerializeField] GameObject viewObject = null!;
        [SerializeField] TextMeshProUGUI downloadPercentText = null!;
        [SerializeField] Image gaugeImage = null!;

        public float CurrentFillAmount => gaugeImage.fillAmount;

        void Awake()
        {
           Hide(); 
        }

        public void SetGaugeValue(float value)
        {
            downloadPercentText.text = $"{(int)(value * 100)}%";
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