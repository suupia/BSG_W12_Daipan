#nullable enable
using Daipan.Battle.scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace Daipan.Stream.MonoScripts
{
    public sealed class WaveProgressViewMono : MonoBehaviour
    {
        [SerializeField] Slider waveProgressSlider = null!;
        
        WaveProgress _waveProgress = null!;
        void Update()
        {
            waveProgressSlider.value = (float)_waveProgress.CurrentProgressRatio;
            // Debug.Log($"waveProgressSlider.value: {waveProgressSlider.value}");
        }

        [Inject]
        public void Initialize(
            WaveProgress timer
            )
        {
            _waveProgress = timer;
        }
    }
}