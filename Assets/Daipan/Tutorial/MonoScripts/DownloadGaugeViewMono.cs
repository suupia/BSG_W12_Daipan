#nullable enable
using UnityEngine;
using UnityEngine.UI;

namespace Daipan.Tutorial.MonoScripts
{
    public class DownloadGaugeViewMono : MonoBehaviour
    {
        [SerializeField] Image gaugeImage = null!;


        public void SetGaugeValue(float value)
        {
            gaugeImage.fillAmount = value;
        }
    }
}