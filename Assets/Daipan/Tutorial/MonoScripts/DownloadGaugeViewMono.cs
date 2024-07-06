#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Daipan.Tutorial.MonoScripts
{
    public class DownloadGaugeViewMono : MonoBehaviour
    {
        [SerializeField] Image gaugeImage = null!;

        public float CurrentFillAmount => gaugeImage.fillAmount;

        void Awake()
        {
           gaugeImage.fillAmount = 0; 
        }

        public void SetGaugeValue(float value)
        {
            gaugeImage.fillAmount = value;
        }
    }
}